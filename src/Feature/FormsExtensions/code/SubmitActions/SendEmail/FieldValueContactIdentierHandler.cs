using System;
using System.Collections;
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
            // ReSharper disable once PossibleInvalidOperationException
            var field = GetFieldById(data.FieldEmailAddressId.Value, formSubmitContext.Fields);
            if (field is null)
            {
                logger.LogWarn($"Could not find field with id {data.FieldEmailAddressId}");
            }
            var contacts = new List<ContactIdentifier>();
            var toAddresses = GetValue(field);
            foreach (var addressStrings in toAddresses)
            {
                contacts.AddRange(addressStrings.Split(';').Select(splittedAddress => GetOrCreateContact(splittedAddress, data.UpdateCurrentContact)));
            }
            return contacts;
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

        private static IEnumerable<string> GetValue(object field)
        {
            var valueObject = field?.GetType().GetProperty("Value")?.GetValue(field, null);
            if (valueObject == null)
            {
                return new List<string>();
            }
            if (valueObject is IList list)
            {
                return (from object valueItemObject in list select valueItemObject.ToString()).ToList();
            }
            return new List<string> {valueObject.ToString()};
        }

    }
}