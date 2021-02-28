using System;
using Feature.FormsExtensions.XDb.Model;
using Feature.FormsExtensions.XDb.Repository;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Analytics;
using Sitecore.Analytics.Tracking.Identification;
using Sitecore.DependencyInjection;
using Sitecore.XConnect;
using Contact = Sitecore.Analytics.Tracking.Contact;
using Facet = Sitecore.XConnect.Facet;

namespace Feature.FormsExtensions.XDb
{
    public class XDbService : IXDbService
    {
        private readonly IXDbContactRepository xDbContactRepository;

        public XDbService(IXDbContactRepository xDbContactRepository)
        {
            this.xDbContactRepository = xDbContactRepository;
        }

        public void IdentifyCurrent(IXDbContact contact)
        {
            CheckIdentifier(contact);
            if (Tracker.Current?.Session != null)
            {
                var identificationManager = ServiceLocator.ServiceProvider.GetRequiredService<IContactIdentificationManager>();
                identificationManager.IdentifyAs(new KnownContactIdentifier(contact.IdentifierSource, contact.IdentifierValue));
            }
        }

        public void ReloadContactDataIntoSession()
        {
            xDbContactRepository.ReloadContactDataIntoSession();
        }
        
        public void UpdateOrCreateServiceContact(IXDbContactWithEmail contact)
        {
            CheckIdentifier(contact);
            xDbContactRepository.UpdateOrCreateXDbServiceContactWithEmail(contact);
        }

        public void UpdateCurrentContactFacet<T>(string facetKey, Action<T> updateFacets) where T : Facet, new()
        {
            UpdateCurrentContactFacet(facetKey, updateFacets, ()=>new T());
        }

        public void UpdateCurrentContactFacet<T>(string facetKey, Action<T> updateFacets, Func<T> createFacet) where T : Facet
        {
            var currentContact = this.GetCurrentContact();
            if (currentContact == null)
                return;
            if (currentContact.IsNew)
            {
                xDbContactRepository.SaveNewContactToCollectionDb(currentContact);
            }
            var trackerIdentifier = new IdentifiedContactReference(Sitecore.Analytics.XConnect.DataAccess.Constants.IdentifierSource, currentContact.ContactId.ToString("N"));
            xDbContactRepository.UpdateContactFacet(trackerIdentifier, new ContactExpandOptions(facetKey), updateFacets, createFacet);
        }
        
        public Guid? GetCurrentContactId()
        {
            var currentContact = this.GetCurrentContact();
            if (currentContact == null)
                return null;
            if (currentContact.IsNew)
            {
                xDbContactRepository.SaveNewContactToCollectionDb(currentContact);
            }
            var trackerIdentifier = new IdentifiedContactReference(Sitecore.Analytics.XConnect.DataAccess.Constants.IdentifierSource, currentContact.ContactId.ToString("N"));
            return xDbContactRepository.GetContactId(trackerIdentifier);
        }

        public Contact GetCurrentContact()
        {
            InitializeTracker();
            if (Tracker.Current == null || Tracker.Current.Contact == null)
                return null;
            return Tracker.Current.Contact;
        }

        private static void InitializeTracker()
        {
            if (Tracker.Current == null && Tracker.Enabled)
            {
                Tracker.StartTracking();
            }
        }

        private static void CheckIdentifier(IXDbContact contact)
        {
            if (string.IsNullOrEmpty(contact.IdentifierSource) || string.IsNullOrEmpty(contact.IdentifierValue))
            {
                throw new Exception("A contact must have an identifiersource and identifiervalue!");
            }
        }
    }
    
}