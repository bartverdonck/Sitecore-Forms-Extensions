using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers
{
    public class XDbNumberTokenHandler : PreferredPhoneNumbersTokenHandler {
        public XDbNumberTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }


        protected override ITokenHandlerResult GetTokenValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.Number))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(phoneNumber.Number);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.Number = value);
            }
        }
        
    }
}