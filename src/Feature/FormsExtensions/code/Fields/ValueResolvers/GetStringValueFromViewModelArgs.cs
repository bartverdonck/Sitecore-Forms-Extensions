using Sitecore.ExperienceForms.Models;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class GetStringValueFromViewModelArgs : MvcPipelineArgs
    {
        public IViewModel FieldViewModel { get; }
        public string Value { get; set; }

        public GetStringValueFromViewModelArgs(IViewModel fieldViewModel)
        {
            FieldViewModel = fieldViewModel;
        }
    }
}