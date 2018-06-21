using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbJobTitleTokenHandler : PersonalInformationTokenHandler
    {

        public XDbJobTitleTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.JobTitle))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.JobTitle);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string jobTitle)
            {
                UpdateFacet( x => x.JobTitle = jobTitle);
            }
        }
        
    }
    
}