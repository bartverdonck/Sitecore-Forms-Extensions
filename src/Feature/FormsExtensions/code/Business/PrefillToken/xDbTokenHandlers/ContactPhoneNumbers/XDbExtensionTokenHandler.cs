using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers
{
    public class XDbExtensionTokenHandler : PreferredPhoneNumbersTokenHandler
    {
        public XDbExtensionTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }


        protected override ITokenHandlerResult GetTokenValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.Extension))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(phoneNumber.Extension);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.Extension = value);
            }
        }

    }
}