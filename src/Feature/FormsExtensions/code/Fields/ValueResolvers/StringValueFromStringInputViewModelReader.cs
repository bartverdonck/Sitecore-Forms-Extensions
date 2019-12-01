using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class StringValueFromStringInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<string> stringInputViewModel)
            {
                args.Value = stringInputViewModel.Value;
                args.AbortPipeline();
            }
        }
    }
}