using System.Collections.Generic;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class StringValueFromStringListInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<List<string>> listStringInputViewModel)
            {
                args.Value = listStringInputViewModel.Value == null ? string.Empty : string.Join(", ", listStringInputViewModel.Value);
                args.AbortPipeline();
            }
        }
    }
}