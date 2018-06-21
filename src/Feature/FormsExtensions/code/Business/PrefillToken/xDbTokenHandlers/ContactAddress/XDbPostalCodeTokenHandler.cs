using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbPostalCodeTokenHandler : PreferredAddressTokenHandler
    {
        public XDbPostalCodeTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.PostalCode))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.PostalCode);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.PostalCode = value);
            }
        }

    }
}