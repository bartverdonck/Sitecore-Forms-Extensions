using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Processing;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class FixedAddressContactIdentierHandler : IExtractSendToContactIdentierHandler
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;

        public FixedAddressContactIdentierHandler(ILogger logger, IXDbService xDbService)
        {
            this.logger = logger;
            this.xDbService = xDbService;
        }

        public IList<ContactIdentifier> GetContacts(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            if (string.IsNullOrEmpty(data.FixedEmailAddress))
            {
                logger.LogWarn("To address is empty");
                return new List<ContactIdentifier>();
            }
            return data.FixedEmailAddress.Split(';').Select(GetContactIdentifier).ToList();
        }

        protected virtual ContactIdentifier GetContactIdentifier(string address)
        {
            IServiceContact serviceContact = new ServiceContact(address);
            xDbService.CreateIfNotExists(serviceContact);
            return new ContactIdentifier(serviceContact.IdentifierSource, serviceContact.IdentifierValue, ContactIdentifierType.Known);
        }
    }
}