using System.Collections.Generic;
using System.Linq;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Processing;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class CurrentContactContactIdentierHandler : IExtractSendToContactIdentierHandler
    {
        private readonly ITracker tracker;
        private readonly ILogger logger;

        public CurrentContactContactIdentierHandler(ILogger logger)
        {
            tracker = Tracker.Current;
            this.logger = logger;
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
            var contact = tracker.Contact;
            var contactIdentifier = contact?.Identifiers.FirstOrDefault(c => c.Type == ContactIdentificationLevel.Known);
            return contactIdentifier == null ? null : new ContactIdentifier(contactIdentifier.Source, contactIdentifier.Identifier, ContactIdentifierType.Known);
        }
    }
}