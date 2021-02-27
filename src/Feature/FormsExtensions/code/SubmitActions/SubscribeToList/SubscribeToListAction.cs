using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ExperienceForms.Diagnostics;
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
        private readonly ISubscriptionService subscriptionService;

        public SubscribeToListAction(ISubmitActionData submitActionData) : this(submitActionData,
            ServiceLocator.ServiceProvider.GetService<ILogger>(), ServiceLocator.ServiceProvider.GetService<IXDbService>(), ServiceLocator.ServiceProvider.GetService<ISubscriptionService>())
        {
        }

        public SubscribeToListAction(ISubmitActionData submitActionData, ILogger logger, IXDbService xDbService, ISubscriptionService subscriptionService) : base(submitActionData)
        {
            this.logger = logger;
            this.xDbService = xDbService;
            this.subscriptionService = subscriptionService;
        }

        protected override bool Execute(SubscribeToListData data, FormSubmitContext formSubmitContext)
        {
            if (data == null)
            {
                logger.LogError("Subscribe To List action is missing configuration.");
                return true;
            }

            if (data.ListId == Guid.Empty)
            {
                logger.Warn("No list was configured.");
                return true; //we will not crash on this
            }            
            
            var contactId = xDbService.GetCurrentContactId();
            if (contactId == null)
            {
                logger.Debug("The current contact is not yet identified and present in xDB. Please use the identify contact action first.");
                return false;
            }

            if (data.CheckedRequirementFieldId.HasValue)
            {
                var checkedRequirementField = GetFieldById(data.CheckedRequirementFieldId.Value, formSubmitContext.Fields);
                if(!IsChecked(checkedRequirementField))
                {
                    return true;
                }
            }

            if (subscriptionService == null)
            {
                logger.LogError("Couldn't resolve ISubscriptionService.");
                return true;
            }

            subscriptionService.Subscribe(data.ListId, contactId.Value);

            return true;
        }

        private static IViewModel GetFieldById(Guid id, IEnumerable<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
        }

        private static bool IsChecked(object field)
        {
            return (bool) (field?.GetType().GetProperty("Value")?.GetValue(field, null) ?? false);
        }

    }
}