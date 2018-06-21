using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers
{
    public class XDbCountryCodeTokenHandler : PreferredPhoneNumbersTokenHandler
    {
        public XDbCountryCodeTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }


        protected override ITokenHandlerResult GetTokenValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.CountryCode))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(phoneNumber.CountryCode);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.CountryCode = value);
            }
        }

    }
}