using System.Collections.Generic;
using Sitecore.Pipelines;

namespace Feature.FormsExtensions.Pipelines.PrefillForm
{
    public class PrefillTokenMapFactory : IPrefillTokenMapFactory
    {
        private Dictionary<string, IPrefillFormTokenHandler> prefillTokenMap;

        public Dictionary<string, IPrefillFormTokenHandler> GetPrefillTokenMap()
        {
            if (prefillTokenMap == null)
            {
                var prefillFormArgs = new PrefillFormArgs();
                CorePipeline.Run("forms.loadPrefillTokenHandlers", prefillFormArgs, false);
                prefillTokenMap = prefillFormArgs.TokenHandlers;
            }
            return prefillTokenMap;
        }
    }
}