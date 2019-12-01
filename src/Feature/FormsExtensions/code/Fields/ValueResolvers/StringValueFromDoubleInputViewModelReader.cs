using System.Globalization;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class StringValueFromDoubleInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<double?> doubleInputViewModel)
            {
                args.Value = doubleInputViewModel.Value.HasValue
                    ? doubleInputViewModel.Value.Value.ToString(CultureInfo.InvariantCulture)
                    : string.Empty;
                args.AbortPipeline();
            }
        }
    }
}