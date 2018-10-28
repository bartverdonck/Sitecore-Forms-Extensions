using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Processing;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class FixedAddressContactIdentierHandler : IExtractSendToContactIdentierHandler
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;
        private readonly IXDbContactFactory xDbContactFactory;

        public FixedAddressContactIdentierHandler(ILogger logger, IXDbService xDbService, IXDbContactFactory xDbContactFactory)
        {
            this.logger = logger;
            this.xDbService = xDbService;
            this.xDbContactFactory = xDbContactFactory;
        }

        public IList<ContactIdentifier> GetContacts(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            if (string.IsNullOrEmpty(data.FixedEmailAddress))
            {
                logger.LogWarn("To address is empty");
                return new List<ContactIdentifier>();
            }
            return data.FixedEmailAddress.Split(';').Where(x=>!string.IsNullOrEmpty(x)).Select(GetContactIdentifier).ToList();
        }

        protected virtual ContactIdentifier GetContactIdentifier(string address)
        {
            var serviceContact = xDbContactFactory.CreateContactWithEmail(address);
            xDbService.UpdateOrCreateServiceContact(serviceContact);
            return new ContactIdentifier(serviceContact.IdentifierSource, serviceContact.IdentifierValue, ContactIdentifierType.Known);
        }
    }
}