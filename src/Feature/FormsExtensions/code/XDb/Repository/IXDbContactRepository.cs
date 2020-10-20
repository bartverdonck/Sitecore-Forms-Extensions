using System;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.XConnect;
using Facet = Sitecore.XConnect.Facet;

namespace Feature.FormsExtensions.XDb.Repository
{
    public interface IXDbContactRepository
    {
        Guid? GetContactId(IdentifiedContactReference reference);
        void UpdateXDbContactEmail(IXDbContactWithEmail contact);
        void UpdateOrCreateXDbServiceContactWithEmail(IXDbContactWithEmail contact);
        void UpdateContactFacet<T>(IdentifiedContactReference reference, ContactExpandOptions expandOptions,
            Action<T> updateFacets, Func<T> createFacet) where T : Facet;
        void SaveNewContactToCollectionDb(Sitecore.Analytics.Tracking.Contact contact);
        void ReloadContactDataIntoSession();
    }
}
