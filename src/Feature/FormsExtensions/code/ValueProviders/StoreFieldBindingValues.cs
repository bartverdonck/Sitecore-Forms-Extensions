using Feature.FormsExtensions.Fields.Bindings;
using Feature.FormsExtensions.XDb;
using Sitecore.ExperienceForms.Mvc.Pipelines.ExecuteSubmit;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.ValueProviders
{
    public class StoreFieldBindingValues : MvcPipelineProcessor<ExecuteSubmitActionsEventArgs>
    {
        private readonly IFieldValueBinderMapFactory fieldValueBinderMapFactory;
        private readonly IXDbService xDbService;

        public StoreFieldBindingValues(IFieldValueBinderMapFactory fieldValueBinderMapFactory, IXDbService xDbService)
        {
            this.fieldValueBinderMapFactory = fieldValueBinderMapFactory;
            this.xDbService = xDbService;
        }

        public override void Process(ExecuteSubmitActionsEventArgs args)
        {
            if (args?.FormSubmitContext?.Fields == null)
            {
                return;
            }
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
                var bindingHandler = fieldValueBinderMapFactory.GetBindingHandler(bindingSettings.ValueProviderSettings);
                
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
                bindingHandler.StoreValue(value);
                valuesUpdated = true;
            }
            if(valuesUpdated)
                xDbService.ReloadContactDataIntoSession();
        }
    }
}