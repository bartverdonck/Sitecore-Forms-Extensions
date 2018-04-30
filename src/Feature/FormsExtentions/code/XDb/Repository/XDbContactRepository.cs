using Feature.FormsExtentions.XDb.Model;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtentions.XDb.Repository
{
    public class XDbContactRepository : IXDbContactRepository
    {
        public void UpdateXDbContact(IBasicContact basicContact)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var reference = new IdentifiedContactReference(basicContact.IdentifierSource, basicContact.IdentifierValue);
                var xDbContact = client.Get(reference, new ContactExpandOptions(CollectionModel.FacetKeys.PersonalInformation, CollectionModel.FacetKeys.EmailAddressList));
                SetPersonalInformation(xDbContact, basicContact, client);
                SetEmail(xDbContact, basicContact, client);
                client.Submit();
            }
        }

        public void UpdateServiceContact(IServiceContact serviceContact)
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
            }
        }

        private static void SetPersonalInformation(Contact contact, IBasicContact basicContact, IXdbContext client)
        {
            if (string.IsNullOrEmpty(basicContact.FirstName) && string.IsNullOrEmpty(basicContact.LastName))
            {
                return;
            }
            var personalInfoFacet = contact.Personal() ?? new PersonalInformation();
            if (personalInfoFacet.FirstName == basicContact.FirstName && personalInfoFacet.LastName == basicContact.LastName)
            {
                return;
            }
            personalInfoFacet.FirstName = basicContact.FirstName;
            personalInfoFacet.LastName = basicContact.LastName;
            client.SetPersonal(contact, personalInfoFacet);
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