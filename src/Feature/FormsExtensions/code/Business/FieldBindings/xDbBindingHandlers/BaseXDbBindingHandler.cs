using System;
using Feature.FormsExtensions.XDb;
using Sitecore.Analytics;
using Sitecore.Analytics.XConnect.Facets;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers
{
    public abstract class BaseXDbBindingHandler<T> : IBindingHandler where T : Facet
    {
        protected abstract string GetFacetKey();
        protected abstract IBindingHandlerResult GetFieldBindingValueFromFacet(T facet);
        protected abstract T CreateFacet();
        private readonly IXDbService xDbService;

        protected BaseXDbBindingHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        public IBindingHandlerResult GetBindingValue()
        {
            if (Tracker.Current?.Contact == null)
                return new NoBindingValueFoundResult();
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            if (xConnectFacet == null)
            {
                return new NoBindingValueFoundResult();
            }
            if (!xConnectFacet.Facets.ContainsKey(GetFacetKey()))
            {
                return new NoBindingValueFoundResult();
            }
            if (!(xConnectFacet.Facets[GetFacetKey()] is T facet))
            {
                return new NoBindingValueFoundResult();
            }
            return GetFieldBindingValueFromFacet(facet);
        }
        
        public abstract void StoreBindingValue(object newValue);

        protected void UpdateFacet(Action<T> updateFacet)
        {
            xDbService.UpdateCurrentContactFacet(GetFacetKey(), updateFacet, CreateFacet);
        }

        
    }
}