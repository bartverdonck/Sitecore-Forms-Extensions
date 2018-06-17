using Feature.FormsExtensions.Business.PrefillToken;
using Feature.FormsExtensions.Fields.Prefill;
using Feature.FormsExtensions.XDb;
using Sitecore.ExperienceForms.Mvc.Pipelines.ExecuteSubmit;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.ExecuteSubmit
{
    public class StorePrefillTokenValues : MvcPipelineProcessor<ExecuteSubmitActionsEventArgs>
    {
        private readonly IPrefillTokenMapFactory prefillTokenMapFactory;
        private readonly IXDbService xDbService;

        public StorePrefillTokenValues(IPrefillTokenMapFactory prefillTokenMapFactory, IXDbService xDbService)
        {
            this.prefillTokenMapFactory = prefillTokenMapFactory;
            this.xDbService = xDbService;
        }

        public override void Process(ExecuteSubmitActionsEventArgs args)
        {
            var tokenMap = prefillTokenMapFactory.GetPrefillTokenMap();
            if (tokenMap == null)
                return;
            var valuesUpdated = false;
            foreach (var fieldModel in args.FormSubmitContext.Fields)
            {
                if (!(fieldModel is IPrefillToken prefillTokenModel))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(prefillTokenModel.PrefillToken))
                {
                    continue;
                }
                if (!tokenMap.ContainsKey(new PrefillTokenKey(prefillTokenModel.PrefillToken)))
                {
                    continue;
                }
                var tokenHandler = tokenMap[new PrefillTokenKey(prefillTokenModel.PrefillToken)];

                var property = fieldModel.GetType().GetProperty("Value");
                if (property == null)
                {
                    continue;
                }

                var value = property.GetValue(fieldModel);
                if (value != null) { 
                    tokenHandler.StoreTokenValue(value);
                    valuesUpdated = true;
                }
            }
            if(valuesUpdated)
                xDbService.ReloadContactDataIntoSession();
        }
    }
}