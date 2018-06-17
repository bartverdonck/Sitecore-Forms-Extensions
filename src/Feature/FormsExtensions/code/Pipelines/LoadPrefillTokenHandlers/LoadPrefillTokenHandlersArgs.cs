using System.Collections.Generic;
using Feature.FormsExtensions.Business.PrefillToken;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.LoadPrefillTokenHandlers
{
    public class LoadPrefillTokenHandlersArgs : MvcPipelineArgs
    {
        public Dictionary<PrefillTokenKey, IPrefillTokenHandler> TokenHandlers { get; set; }

        public LoadPrefillTokenHandlersArgs()
        {
            TokenHandlers = new Dictionary<PrefillTokenKey, IPrefillTokenHandler>();
        }
    }
}