using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public class XDbCityTokenHandler : PreferredAddressTokenHandler
    {
        public XDbCityTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.City))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(addres.City);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string city)
            {
                UpdateFacet(x => x.PreferredAddress.City = city);
            }
        }

    }
}