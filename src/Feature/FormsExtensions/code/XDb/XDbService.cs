using System;
using Feature.FormsExtensions.XDb.Model;
using Feature.FormsExtensions.XDb.Repository;
using Sitecore.Analytics;
using Sitecore.XConnect;
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
                Tracker.Current.Session.IdentifyAs(contact.IdentifierSource, contact.IdentifierValue);
            }
        }

        public void ReloadContactDataIntoSession()
        {
            xDbContactRepository.ReloadContactDataIntoSession();
        }

        public void UpdateEmail(IXDbContactWithEmail contact)
        {
            CheckIdentifier(contact);
            xDbContactRepository.UpdateXDbContactEmail(contact);
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
            if (Tracker.Current == null || Tracker.Current.Contact == null)
                return;
            if (Tracker.Current.Contact.IsNew)
            {
                xDbContactRepository.SaveNewContactToCollectionDb(Tracker.Current.Contact);
            }
            var trackerIdentifier = new IdentifiedContactReference(Sitecore.Analytics.XConnect.DataAccess.Constants.IdentifierSource, Sitecore.Analytics.Tracker.Current.Contact.ContactId.ToString("N"));
            xDbContactRepository.UpdateContactFacet(trackerIdentifier, new ContactExpandOptions(facetKey), updateFacets, createFacet);
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