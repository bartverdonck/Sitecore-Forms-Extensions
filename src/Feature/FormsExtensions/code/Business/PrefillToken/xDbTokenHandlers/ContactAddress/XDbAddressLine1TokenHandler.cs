using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbAddressLine1TokenHandler: PreferredAddressTokenHandler
    {
        public XDbAddressLine1TokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine1))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.AddressLine1);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string addressLine1)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine1=addressLine1);
            }
        }

    }
}