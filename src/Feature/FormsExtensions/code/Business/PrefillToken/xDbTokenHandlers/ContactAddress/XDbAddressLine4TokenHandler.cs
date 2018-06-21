using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbAddressLine4TokenHandler : PreferredAddressTokenHandler
    {
        public XDbAddressLine4TokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine4))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.AddressLine4);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string addressLine4)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine4 = addressLine4);
            }
        }
    }
}