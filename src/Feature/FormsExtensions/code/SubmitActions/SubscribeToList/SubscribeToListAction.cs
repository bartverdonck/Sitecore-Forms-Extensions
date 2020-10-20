using System;
using Feature.FormsExtensions.XDb;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.ListManagement.XConnect.Web;

namespace Feature.FormsExtensions.SubmitActions.SubscribeToList
{
    public class SubscribeToListAction : SubmitActionBase<SubscribeToListData>
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;

        public SubscribeToListAction(ISubmitActionData submitActionData) : this(submitActionData,
            ServiceLocator.ServiceProvider.GetService<ILogger>(), ServiceLocator.ServiceProvider.GetService<IXDbService>())
        {
        }

        public SubscribeToListAction(ISubmitActionData submitActionData, ILogger logger, IXDbService xDbService) : base(submitActionData)
        {
            this.logger = logger;
            this.xDbService = xDbService;
        }

        protected override bool Execute(SubscribeToListData data, FormSubmitContext formSubmitContext)
        {
            if (data.ListId == Guid.Empty)
            {
                logger.LogWarn("No list was configured.");
                return true; //we will not crash on this
            }            
            
            var contactId = xDbService.GetCurrentContactId();
            if (contactId == null)
            {
                logger.LogDebug("The current contact is not yet identified and present in xDB. Please use the identify contact action first.");
                return false;
            }

            var subscriptionService = ServiceLocator.ServiceProvider.GetService<ISubscriptionService>();
            subscriptionService.Subscribe(data.ListId, contactId.Value);

            return true;
        }

    }
}