using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactConcent
{
    public class XDbConsentRevokedTokenHandler : ConsentInformationTokenHandler
    {
        public XDbConsentRevokedTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(ConsentInformation facet)
        {
            return new TokenValueFoundResult(facet.ConsentRevoked);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x=>x.ConsentRevoked=value);
            }
        }
    }
}