using Feature.FormsExtensions.Business.FieldBindings;
using Sitecore.Data;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderForm;
using Sitecore.Mvc.Pipelines;
using Sitecore.Rules;

namespace Feature.FormsExtensions.Pipelines.ShowFormPage
{
    public class ValueProviderConditionsChecker : MvcPipelineProcessor<RenderFormEventArgs> {
        public override void Process(RenderFormEventArgs args)
        {
            var formId = new ID(args.ViewModel.ItemId);
            var formItem = Sitecore.Context.Database.GetItem(formId);
            if (formItem == null)
            {
                return;
            }
            var conditionsItemId = formItem["ValueProviderConditions"];
            if (string.IsNullOrWhiteSpace(conditionsItemId))
            {
                ValueProviderContext.ValueProviderConditionsMet = true;
                return;
            }

            var conditionsItem = Sitecore.Context.Database.GetItem(new ID(conditionsItemId));
            var rc = new ValueProviderConditionsRulesContext();
            var rules = RuleFactory.GetRules<ValueProviderConditionsRulesContext>(new []{conditionsItem}, "Conditions");
            ValueProviderContext.ValueProviderConditionsMet = rules.EvaluateRules(rc);
        }
    }
}