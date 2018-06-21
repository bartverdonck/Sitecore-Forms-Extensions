using System;
using Feature.FormsExtensions.XDb;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers
{
    public abstract class BaseXDbTokenHandler<T> : IPrefillTokenHandler where T : Facet
    {
        protected abstract string GetFacetKey();
        protected abstract ITokenHandlerResult GetTokenValueFromFacet(T facet);
        protected abstract T CreateFacet();
        private readonly IXDbService xDbService;

        protected BaseXDbTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        public ITokenHandlerResult GetTokenValue()
        {
            if (Tracker.Current?.Contact == null)
                return new NoTokenValueFoundResult();
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            if (xConnectFacet == null)
            {
                return new NoTokenValueFoundResult();
            }
            if (!xConnectFacet.Facets.ContainsKey(GetFacetKey()))
            {
                return new NoTokenValueFoundResult();
            }
            if (!(xConnectFacet.Facets[GetFacetKey()] is T facet))
            {
                return new NoTokenValueFoundResult();
            }
            return GetTokenValueFromFacet(facet);
        }
        
        public abstract void StoreTokenValue(object newValue);

        protected void UpdateFacet(Action<T> updateFacet)
        {
            xDbService.UpdateCurrentContactFacet(GetFacetKey(), updateFacet, CreateFacet);
        }

        
    }
}