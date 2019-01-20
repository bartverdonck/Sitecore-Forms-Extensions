using Feature.FormsExtensions.Business.FieldBindings;
using Feature.FormsExtensions.Fields.Bindings;
using Feature.FormsExtensions.XDb;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Mvc.Pipelines.ExecuteSubmit;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.ExecuteSubmit
{
    public class StoreFieldBindingValues : MvcPipelineProcessor<ExecuteSubmitActionsEventArgs>
    {
        private readonly IFieldBindingMapFactory fieldBindingMapFactory;
        private readonly IXDbService xDbService;

        public StoreFieldBindingValues(IFieldBindingMapFactory fieldBindingMapFactory, IXDbService xDbService)
        {
            this.fieldBindingMapFactory = fieldBindingMapFactory;
            this.xDbService = xDbService;
        }

        public override void Process(ExecuteSubmitActionsEventArgs args)
        {
            var valuesUpdated = false;
            foreach (var fieldModel in args.FormSubmitContext.Fields)
            {
                if (!(fieldModel is IBindingSettings bindingSettings))
                {
                    continue;
                }
                if (!bindingSettings.StoreBindingValue) { 
                    continue;
                }
                if (string.IsNullOrEmpty(bindingSettings.ValueProviderSettings?.ValueProviderItemId))
                {
                    continue;
                }
                var bindingHandler = fieldBindingMapFactory.GetBindingHandler(bindingSettings.ValueProviderSettings);
                
                var property = fieldModel.GetType().GetProperty("Value");
                if (property == null)
                {
                    continue;
                }

                var value = property.GetValue(fieldModel);
                if (value == null)
                {
                    continue;
                }
                bindingHandler.StoreBindingValue(value);
                valuesUpdated = true;
            }
            if(valuesUpdated)
                xDbService.ReloadContactDataIntoSession();
        }
    }
}