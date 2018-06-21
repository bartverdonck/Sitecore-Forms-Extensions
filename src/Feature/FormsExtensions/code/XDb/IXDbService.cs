using System;
using Feature.FormsExtensions.XDb.Model;
using Facet = Sitecore.XConnect.Facet;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbService
    {
        void IdentifyCurrent(IXDbContact contact);
        void UpdateEmail(IXDbContactWithEmail contact);
        void UpdateOrCreateServiceContact(IXDbContactWithEmail contact);
        void UpdateCurrentContactFacet<T>(string facetKey, Action<T> updateFacet) where T : Facet, new();
        void UpdateCurrentContactFacet<T>(string facetKey, Action<T> updateFacets, Func<T> createFacet) where T : Facet;
        void ReloadContactDataIntoSession();
    }

}
