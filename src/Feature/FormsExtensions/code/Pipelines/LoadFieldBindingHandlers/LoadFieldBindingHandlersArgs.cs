using System.Collections.Generic;
using Feature.FormsExtensions.Business.FieldBindings;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.LoadFieldBindingHandlers
{
    public class LoadFieldBindingHandlersArgs : MvcPipelineArgs
    {
        public Dictionary<FieldBindingTokenKey, IBindingHandler> FieldBindingHandlers { get; set; }

        public LoadFieldBindingHandlersArgs()
        {
            FieldBindingHandlers = new Dictionary<FieldBindingTokenKey, IBindingHandler>();
        }
    }
}