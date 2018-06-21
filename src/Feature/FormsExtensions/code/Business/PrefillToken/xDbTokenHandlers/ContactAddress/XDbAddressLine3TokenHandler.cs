using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbAddressLine3TokenHandler : PreferredAddressTokenHandler
    {
        public XDbAddressLine3TokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine3))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.AddressLine3);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string addressLine3)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine3 = addressLine3);
            }
        }
    }
}