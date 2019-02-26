using Feature.FormsExtensions.Business.ReCaptcha;
using Sitecore;
using Sitecore.Data;
using Sitecore.ExperienceForms.Mvc.Pipelines.ExecuteSubmit;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.ExecuteSubmit
{
    public class RecaptchaExecutedValidation : MvcPipelineProcessor<ExecuteSubmitActionsEventArgs>
    {
        
        public override void Process(ExecuteSubmitActionsEventArgs args)
        {
            if (PageContainsRecaptcha(args)&&!RecaptchaContext.RecaptchaValidated)
            {
                args.AbortPipeline();
            }
        }

        private bool PageContainsRecaptcha(ExecuteSubmitActionsEventArgs args)
        {
            if (string.IsNullOrEmpty(args.FormSubmitContext.ButtonId))
                return false;
            
            var buttonItem = Context.Database.GetItem(new ID(args.FormSubmitContext.ButtonId));
            var page = buttonItem.Parent;
            foreach (var child in page.Axes.GetDescendants())
            {
                if (child.Fields["Field Type"]?.Value == "{E383BDE2-BC88-4278-83EF-832A15C9E94A}")
                {
                    return true;
                }
            }return false;
        }
        
    }
}