using System;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbService
    {
        void IdentifyCurrent(IXDbContact contact);
        void UpdateEmail(IXDbContactWithEmail contact);
        void UpdateOrCreateServiceContact(IXDbContactWithEmail contact);
        void UpdateCurrentContactFacets(ContactExpandOptions expandOptions, Action<Contact> updateFacets);
        void ReloadContactDataIntoSession();
    }

}
