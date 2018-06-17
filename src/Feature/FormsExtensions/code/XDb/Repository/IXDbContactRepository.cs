using System;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.XDb.Repository
{
    public interface IXDbContactRepository
    {
        void UpdateXDbContactEmail(IXDbContactWithEmail contact);

        void UpdateOrCreateXDbServiceContactWithEmail(IXDbContactWithEmail contact);

        void UpdateContactFacets(IdentifiedContactReference reference, ContactExpandOptions expandOptions, Action<Contact> updateFacets);
        void SaveNewContactToCollectionDb(Sitecore.Analytics.Tracking.Contact contact);
        void ReloadContactDataIntoSession();
    }
}
