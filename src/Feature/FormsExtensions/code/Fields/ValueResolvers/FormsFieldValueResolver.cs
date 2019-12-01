using Sitecore.ExperienceForms.Models;
using Sitecore.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class FormsFieldValueResolver : IFormsFieldValueResolver
    {
        public string GetStringFieldValue(IViewModel fieldViewModel)
        {
            var getStringValueFromViewModelArgs = new GetStringValueFromViewModelArgs(fieldViewModel);
            CorePipeline.Run("formsextensions.getStringValueFromViewModel", getStringValueFromViewModelArgs, false);
            return getStringValueFromViewModelArgs.Value;
        }
    }
}