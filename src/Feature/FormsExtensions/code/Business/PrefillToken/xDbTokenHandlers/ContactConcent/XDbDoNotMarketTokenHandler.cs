using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactConcent
{
    public class XDbDoNotMarketTokenHandler : ConsentInformationTokenHandler
    {
        public XDbDoNotMarketTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(ConsentInformation facet)
        {
            return new TokenValueFoundResult(facet.DoNotMarket);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x => x.DoNotMarket = value);
            }
        }
    }
}