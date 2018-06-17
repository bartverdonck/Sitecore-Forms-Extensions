using System.Collections.Generic;
using Feature.FormsExtensions.Pipelines.LoadPrefillTokenHandlers;
using Sitecore.Pipelines;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    public class PrefillTokenMapFactory : IPrefillTokenMapFactory
    {
        private Dictionary<PrefillTokenKey, IPrefillTokenHandler> prefillTokenMap;

        public Dictionary<PrefillTokenKey, IPrefillTokenHandler> GetPrefillTokenMap()
        {
            if (prefillTokenMap == null)
            {
                var prefillFormArgs = new LoadPrefillTokenHandlersArgs();
                CorePipeline.Run("forms.loadPrefillTokenHandlers", prefillFormArgs, false);
                prefillTokenMap = prefillFormArgs.TokenHandlers;
            }
            return prefillTokenMap;
        }
    }
}