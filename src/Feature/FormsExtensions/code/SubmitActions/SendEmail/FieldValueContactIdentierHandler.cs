using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers;
using Feature.FormsExtensions.XDb;
using Sitecore.Analytics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class FieldValueContactIdentierHandler : IExtractSendToContactIdentierHandler
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;
        private readonly IXDbContactFactory contactFactory;

        public FieldValueContactIdentierHandler(ILogger logger, IXDbService xDbService, IXDbContactFactory contactFactory)
        {
            this.logger = logger;
            this.xDbService = xDbService;
            this.contactFactory = contactFactory;
        }

        public IList<ContactIdentifier> GetContacts(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            var field = GetFieldById(data.FieldEmailAddressId.Value, formSubmitContext.Fields);
            if (field is null)
            {
                logger.LogWarn($"Could not find field with id {data.FieldEmailAddressId}");
            }
            var toAddresses = GetValue(field);
            if (string.IsNullOrEmpty(toAddresses))
            {
                logger.LogWarn("To address from field is empty");
                return new List<ContactIdentifier>();
            }
            return toAddresses.Split(';').Select(x=>GetOrCreateContact(x, data.UpdateCurrentContact)).ToList();
        }

        private ContactIdentifier GetOrCreateContact(string toAddress, bool updateCurrentContact)
        {
            var identifier = updateCurrentContact ? UpdateEmailContact(toAddress) : GetServiceContactIdentifier(toAddress);
            return identifier ?? GetServiceContactIdentifier(toAddress);
        }

        private ContactIdentifier UpdateEmailContact(string toAddress)
        {
            if (Tracker.Current == null || Tracker.Current.Contact == null)
                return null;
            new PreferredEmailBindingHandler().StoreBindingValue(toAddress);
            return new ContactIdentifier(Sitecore.Analytics.XConnect.DataAccess.Constants.IdentifierSource,
                Tracker.Current.Contact.ContactId.ToString("N"), ContactIdentifierType.Anonymous);
        }

        protected EmailAddressList CreateFacet()
        {
            return new EmailAddressList(new EmailAddress("", true), Sitecore.Configuration.Settings.GetSetting("XDbPreferredEmailAddress", "preferred"));
        }

        protected virtual ContactIdentifier GetServiceContactIdentifier(string address)
        {
            var serviceContact = contactFactory.CreateContactWithEmail(address);
            xDbService.UpdateOrCreateServiceContact(serviceContact);
            return new ContactIdentifier(serviceContact.IdentifierSource, serviceContact.IdentifierValue, ContactIdentifierType.Known);
        }

        private static IViewModel GetFieldById(Guid id, IList<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
        }

        private static string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }
    }
}