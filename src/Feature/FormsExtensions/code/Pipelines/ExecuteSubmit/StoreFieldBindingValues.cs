using Feature.FormsExtensions.Business.FieldBindings;
using Feature.FormsExtensions.Fields.Bindings;
using Feature.FormsExtensions.XDb;
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
            var tokenMap = fieldBindingMapFactory.GetFieldBindingTokenMap();
            if (tokenMap == null)
                return;
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
                if (string.IsNullOrEmpty(bindingSettings.BindingToken))
                {
                    continue;
                }
                if (!tokenMap.ContainsKey(new FieldBindingTokenKey(bindingSettings.BindingToken)))
                {
                    continue;
                }
                var tokenHandler = tokenMap[new FieldBindingTokenKey(bindingSettings.BindingToken)];

                var property = fieldModel.GetType().GetProperty("Value");
                if (property == null)
                {
                    continue;
                }

                var value = property.GetValue(fieldModel);
                if (value != null) { 
                    tokenHandler.StoreBindingValue(value);
                    valuesUpdated = true;
                }
            }
            if(valuesUpdated)
                xDbService.ReloadContactDataIntoSession();
        }
    }
}