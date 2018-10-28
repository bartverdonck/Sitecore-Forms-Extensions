using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;

namespace Feature.FormsExtensions.SubmitActions.ShowFormPage
{
    public class ShowFormPageAction : SubmitActionBase<ShowFormPageData>
    {
        private readonly ILogger logger;

        public ShowFormPageAction(ISubmitActionData submitActionData) : this(submitActionData,
            ServiceLocator.ServiceProvider.GetService<ILogger>())
        {
        }

        public ShowFormPageAction(ISubmitActionData submitActionData, ILogger logger) : base(submitActionData)
        {
            this.logger = logger;
        }

        protected override bool Execute(ShowFormPageData data, FormSubmitContext formSubmitContext)
        {
            if (data.FormPageId == null || data.FormPageId == Guid.Empty)
            {
                logger.LogWarn("Empty FormPageId");
                return true; //we will not crash on this
            }
            ShowFormPageContext.FormPage = data.FormPageId;
            return true;
        }

    }
}