using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbGenderTokenHandler : PersonalInformationTokenHandler
    {
        public XDbGenderTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Gender))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.Gender);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string gender)
            {
                UpdateFacet(x=>x.Gender=gender);
            }
        }
        
    }
}