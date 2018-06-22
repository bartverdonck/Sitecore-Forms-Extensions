using System.Collections.Generic;
using Feature.FormsExtensions.Pipelines.LoadFieldBindingHandlers;
using Sitecore.Pipelines;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class FieldBindingMapFactory : IFieldBindingMapFactory
    {
        private Dictionary<FieldBindingTokenKey, IBindingHandler> fieldBindingTokenMap;

        public Dictionary<FieldBindingTokenKey, IBindingHandler> GetFieldBindingTokenMap()
        {
            if (fieldBindingTokenMap == null)
            {
                var prefillFormArgs = new LoadFieldBindingHandlersArgs();
                CorePipeline.Run("forms.loadFieldBindingHandlers", prefillFormArgs, false);
                fieldBindingTokenMap = prefillFormArgs.FieldBindingHandlers;
            }
            return fieldBindingTokenMap;
        }
    }
}