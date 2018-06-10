using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class FieldValueContactIdentierHandler : IExtractSendToContactIdentierHandler
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;

        public FieldValueContactIdentierHandler(ILogger logger, IXDbService xDbService)
        {
            this.logger = logger;
            this.xDbService = xDbService;
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
            return updateCurrentContact ? IdentifyAndUpdateEmailContact(toAddress) : GetServiceContactIdentifier(toAddress);
        }

        private ContactIdentifier IdentifyAndUpdateEmailContact(string toAddress)
        {
            var basicContact = new BasicContact {Email = toAddress};
            xDbService.IdentifyCurrent(basicContact);
            return new ContactIdentifier(basicContact.IdentifierSource, basicContact.IdentifierValue, ContactIdentifierType.Known);
        }

        protected virtual ContactIdentifier GetServiceContactIdentifier(string address)
        {
            IServiceContact serviceContact = new ServiceContact(address);
            xDbService.CreateIfNotExists(serviceContact);
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