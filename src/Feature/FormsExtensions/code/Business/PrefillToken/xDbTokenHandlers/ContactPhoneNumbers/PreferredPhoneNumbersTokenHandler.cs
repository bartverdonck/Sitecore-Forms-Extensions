using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers
{
    public abstract class PreferredPhoneNumbersTokenHandler : BaseXDbTokenHandler<PhoneNumberList>
    {
        protected PreferredPhoneNumbersTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override string GetFacetKey()
        {
            return PhoneNumberList.DefaultFacetKey;
        }

        protected override PhoneNumberList CreateFacet()
        {
            return new PhoneNumberList(new PhoneNumber("",""), Sitecore.Configuration.Settings.GetSetting("XDbPreferredPhoneNumber", "preferred"));
        }
        protected override ITokenHandlerResult GetTokenValueFromFacet(PhoneNumberList facet)
        {
            if (facet.PreferredPhoneNumber == null)
            {
                return new NoTokenValueFoundResult();
            }
            return GetTokenValueFromFacet(facet.PreferredPhoneNumber);
        }

        protected abstract ITokenHandlerResult GetTokenValueFromFacet(PhoneNumber phoneNumber);

    }
}