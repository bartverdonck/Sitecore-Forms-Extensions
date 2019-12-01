using System.Collections.Generic;
using System.Linq;
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
                args.Value = listStringInputViewModel.Value.Aggregate("", (current, value) => current + ", " + value).Remove(0, 2);
                args.AbortPipeline();
            }
        }
    }
}