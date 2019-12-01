using System;
using System.Globalization;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class StringValueFromDateTimeInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<DateTime?> dateTimeInputViewModel)
            {
                args.Value = dateTimeInputViewModel.Value.HasValue ? dateTimeInputViewModel.Value.Value.ToString(CultureInfo.CurrentCulture) : string.Empty;
                args.AbortPipeline();
            }

        }
    }
}