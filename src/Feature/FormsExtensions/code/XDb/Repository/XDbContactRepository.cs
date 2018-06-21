using System;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;
using Contact = Sitecore.XConnect.Contact;
using Facet = Sitecore.XConnect.Facet;

namespace Feature.FormsExtensions.XDb.Repository
{
    public class XDbContactRepository : IXDbContactRepository
    {
        public void UpdateXDbContactEmail(IXDbContactWithEmail basicContact)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var reference = new IdentifiedContactReference(basicContact.IdentifierSource, basicContact.IdentifierValue);
                var xDbContact = client.Get(reference, new ContactExpandOptions(CollectionModel.FacetKeys.PersonalInformation, CollectionModel.FacetKeys.EmailAddressList));
                SetEmail(xDbContact, basicContact, client);
                client.Submit();
            }
        }

        public void UpdateContactFacets(IdentifiedContactReference reference, ContactExpandOptions expandOptions, Action<Contact> updateFacets)
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var xDbContact = client.Get(reference, expandOptions);
                if (xDbContact != null)
                {
                    //xDbContact.GetFacet<PersonalInformation>()
                    updateFacets(xDbContact);
                    client.Submit();
                }
            }
        }
        
        public void UpdateContactFacet<T>(IdentifiedContactReference reference, ContactExpandOptions expandOptions, Action<T> updateFacets, Func<T> createFacet) where T : Facet
        {
            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                var xDbContact = client.Get(reference, expandOptions);
                if (xDbContact != null)
                {
                    var facet = xDbContact.GetFacet<T>();
                    if (facet == null)
                    {
                        facet = createFacet();
                        client.SetFacet(xDbContact, facet);
                    }
                    updateFacets(facet);
                    client.Submit();
                }
            }
        }

        public void SaveNewContactToCollectionDb(Sitecore.Analytics.Tracking.Contact contact)
        {
            if (CreateContactManager() is ContactManager manager)
            {
                contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
                manager.SaveContactToCollectionDb(Tracker.Current.Contact);
            }
        }

        private static object CreateContactManager()
        {
            return Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true);
        }

        public void ReloadContactDataIntoSession()
        {
            if (Tracker.Current.Contact == null)
                return;
            if (CreateContactManager() is ContactManager manager)
            {
                manager.RemoveFromSession(Tracker.Current.Contact.ContactId);
                Tracker.Current.Session.Contact = manager.LoadContact(Tracker.Current.Contact.ContactId);
            }
        }

        public void UpdateOrCreateXDbServiceContactWithEmail(IXDbContactWithEmail serviceContact)
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
        
        private static void SetEmail(Contact contact, IXDbContactWithEmail xDbContact, IXdbContext client)
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