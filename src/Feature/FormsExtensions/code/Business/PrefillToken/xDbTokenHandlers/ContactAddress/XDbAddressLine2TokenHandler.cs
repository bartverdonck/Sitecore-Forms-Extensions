using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbAddressLine2TokenHandler : PreferredAddressTokenHandler
    {
        public XDbAddressLine2TokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine2))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.AddressLine2);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string addressLine2)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine2 = addressLine2);
            }
        }
    }
}