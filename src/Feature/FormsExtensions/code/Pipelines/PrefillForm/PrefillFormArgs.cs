using System.Collections.Generic;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.PrefillForm
{
    public class PrefillFormArgs : MvcPipelineArgs
    {
        public Dictionary<string, IPrefillFormTokenHandler> TokenHandlers { get; set; }

        public PrefillFormArgs()
        {
            TokenHandlers = new Dictionary<string, IPrefillFormTokenHandler>();
        }
    }
}