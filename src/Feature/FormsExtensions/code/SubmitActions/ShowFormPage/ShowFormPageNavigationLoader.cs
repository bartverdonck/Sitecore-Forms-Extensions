using System;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderForm;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.SubmitActions.ShowFormPage
{
    public class ShowFormPageNavigationLoader : MvcPipelineProcessor<RenderFormEventArgs>
    {
        private readonly IFormRenderingContext formRenderingContext;

        public ShowFormPageNavigationLoader(IFormRenderingContext formRenderingContext)
        {
            this.formRenderingContext = formRenderingContext;
        }

        public override void Process(RenderFormEventArgs args)
        {
            var page = ShowFormPageContext.FormPage;
            if (!page.HasValue || page.Value == Guid.Empty)
            {
                return;
            }
            formRenderingContext.NavigationData = new NavigationData
            {
                PageId = page.Value.ToString(),
                Step = 0,
                NavigationType = NavigationType.Submit
            };
        }
    }
}