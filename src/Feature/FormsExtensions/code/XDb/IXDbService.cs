using System;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.Analytics.Tracking;
using Facet = Sitecore.XConnect.Facet;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbService
    {
        void IdentifyCurrent(IXDbContact contact);
        Guid? GetCurrentContactId();
        void UpdateOrCreateServiceContact(IXDbContactWithEmail contact);
        void UpdateCurrentContactFacet<T>(string facetKey, Action<T> updateFacet) where T : Facet, new();
        void UpdateCurrentContactFacet<T>(string facetKey, Action<T> updateFacets, Func<T> createFacet) where T : Facet;
        void ReloadContactDataIntoSession();
        Contact GetCurrentContact();
    }

}
