using Feature.FormsExtensions.XDb.Model;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.XDb.Repository
{
    public class XDbContactRepository : IXDbContactRepository
    {
        public void UpdateXDbContact(IXDbContact basicContact)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var reference = new IdentifiedContactReference(basicContact.IdentifierSource, basicContact.IdentifierValue);
                var xDbContact = client.Get(reference, new ContactExpandOptions(CollectionModel.FacetKeys.PersonalInformation, CollectionModel.FacetKeys.EmailAddressList));
                SetEmail(xDbContact, basicContact, client);
                client.Submit();
            }
        }

        public void UpdateOrCreateXDbContact(IXDbContact serviceContact)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var reference = new IdentifiedContactReference(serviceContact.IdentifierSource, serviceContact.IdentifierValue);
                var contact = client.Get(reference, new ContactExpandOptions(CollectionModel.FacetKeys.EmailAddressList));
                if (contact == null)
                {
                    contact = new Contact(new ContactIdentifier(reference.Source,reference.Identifier,ContactIdentifierType.Known));
                    SetEmail(contact, serviceContact, client);
                    client.AddContact(contact);
                    client.Submit();
                }
                else if (contact.Emails()?.PreferredEmail.SmtpAddress != serviceContact.Email)
                {
                    SetEmail(contact, serviceContact, client);
                    client.Submit();
                }
            }
        }
        
        private static void SetEmail(Contact contact, IXDbContact xDbContact, IXdbContext client)
        {
            if (string.IsNullOrEmpty(xDbContact.Email))
            {
                return;
            }
            var emailFacet = contact.Emails();
            if (emailFacet == null)
            {
                emailFacet = new EmailAddressList(new EmailAddress(xDbContact.Email, false), "Preferred");
            }
            else
            {
                if (emailFacet.PreferredEmail?.SmtpAddress == xDbContact.Email)
                {
                    return;
                }
                emailFacet.PreferredEmail = new EmailAddress(xDbContact.Email, false);
            }
            client.SetEmails(contact, emailFacet);
        }
    }
}