using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbLastNameTokenHandler : PersonalInformationTokenHandler
    {
        public XDbLastNameTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.LastName))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.LastName);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string lastName)
            {
                UpdateFacet(x => x.LastName = lastName);
            }
        }
    }
}