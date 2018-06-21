using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbStateOrProvinceTokenHandler : PreferredAddressTokenHandler
    {
        public XDbStateOrProvinceTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.StateOrProvince))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.StateOrProvince);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.StateOrProvince = value);
            }
        }

    }
}