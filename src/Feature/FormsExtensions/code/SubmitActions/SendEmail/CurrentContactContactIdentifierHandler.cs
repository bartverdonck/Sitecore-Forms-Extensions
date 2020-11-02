using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Processing;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class CurrentContactContactIdentifierHandler : IExtractSendToContactIdentifierHandler
    {
        private readonly ILogger logger;
        private readonly IXDbService xDbService;

        public CurrentContactContactIdentifierHandler(ILogger logger, IXDbService xDbService)
        {
            this.logger = logger;
            this.xDbService = xDbService;
        }

        public IList<ContactIdentifier> GetContacts(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            var contactIdentifier = GetContactIdentifier();
            if (contactIdentifier == null)
            {
                logger.LogWarn("Could not find identified contact.");
                return new List<ContactIdentifier>();
            }
            return new List<ContactIdentifier> {contactIdentifier};
        }

        private ContactIdentifier GetContactIdentifier()
        {
            var contact = xDbService.GetCurrentContact();
            if (contact == null)
            {
                logger.LogWarn("Current contact is null");
                return null;
            }
            var contactIdentifier = contact.Identifiers.FirstOrDefault(c => c.Type == ContactIdentificationLevel.Known);
            return contactIdentifier == null ? null : new ContactIdentifier(contactIdentifier.Source, contactIdentifier.Identifier, ContactIdentifierType.Known);
        }

    }
}