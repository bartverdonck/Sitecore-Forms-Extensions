using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class StringValueFromBooleanInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<bool> booleanInputViewModel)
            {
                args.Value = booleanInputViewModel.Value ? "Checked" : "Not Checked";
                args.AbortPipeline();
            }

        }
    }
}