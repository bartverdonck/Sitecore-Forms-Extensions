using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbFirstNameTokenHandler : PersonalInformationTokenHandler
    {
        public XDbFirstNameTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.FirstName))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.FirstName);
        }
        
        public override void StoreTokenValue(object newValue)
        {
            if(newValue is string firstName) {
                UpdateFacet(x=>x.FirstName=firstName);
            }
        }
    }
}