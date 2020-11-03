using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders;
using Feature.FormsExtensions.XDb;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.StringExtensions;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class FieldValueContactIdentifierHandler : IExtractSendToContactIdentifierHandler
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;
        private readonly IXDbContactFactory contactFactory;

        public FieldValueContactIdentifierHandler(ILogger logger, IXDbService xDbService, IXDbContactFactory contactFactory)
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
            var toAddresses = GetValue(field,data.EmailFieldInDynamicDatasource);
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
            var currentContact = xDbService.GetCurrentContact();
            if (currentContact == null)
                return null;
            new PreferredEmailFieldValueBinder().StoreValue(toAddress);
            return new ContactIdentifier(Sitecore.Analytics.XConnect.DataAccess.Constants.IdentifierSource, currentContact.ContactId.ToString("N"), ContactIdentifierType.Anonymous);
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

        private static IEnumerable<string> GetValue(object field, string emailField)
        {
            var valueObject = field?.GetType().GetProperty("Value")?.GetValue(field, null);
            if (valueObject == null)
            {
                return new List<string>();
            }
            if (valueObject is IList list)
            {
                if (emailField != null && !emailField.IsNullOrEmpty())
                {
                    return GetListValue(field, emailField);
                }
                return (from object valueItemObject in list select valueItemObject.ToString()).ToList();
            }
            return new List<string> {valueObject.ToString()};
        }

        private static IEnumerable<string> GetListValue(object field, string emailField)
        {
            if (field is ListViewModel list)
            {
                foreach (var listFieldItem in list.Items)
                {
                    if (list.Value.Contains(listFieldItem.Value))
                    {
                        var item = Sitecore.Context.Database.GetItem(listFieldItem.ItemId);
                        if (item?[emailField] != null)
                        {
                            yield return item[emailField];
                        }
                    }
                }
            }
        }


    }
}