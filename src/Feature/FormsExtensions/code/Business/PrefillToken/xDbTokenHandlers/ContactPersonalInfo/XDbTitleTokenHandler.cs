using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbTitleTokenHandler : PersonalInformationTokenHandler
    {
        public XDbTitleTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Title))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.Title);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string title)
            {
                UpdateFacet( x => x.Title = title);
            }
        }
        
    }
}