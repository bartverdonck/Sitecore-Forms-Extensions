using Sitecore.Analytics;
using Sitecore.Analytics.Model.Framework;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers
{
    public abstract class BaseXDbTokenHandler<T> : IPrefillTokenHandler where T : class, IFacet
    {
        public ITokenHandlerResult GetTokenValue()
        {
            if (Tracker.Current?.Contact == null)
                return new NoTokenValueFoundResult();
            if (!(Tracker.Current.Contact.Facets[GetFacetKey()] is T facet))
                return new NoTokenValueFoundResult();
            return GetTokenValueFromFacet(facet);
        }

        protected abstract string GetFacetKey();

        protected abstract ITokenHandlerResult GetTokenValueFromFacet(T contactPersonalInfo);

        public abstract void StoreTokenValue(object newValue);
    }
}