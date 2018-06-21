using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbMiddleNameTokenHandler : PersonalInformationTokenHandler
    {
        public XDbMiddleNameTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.MiddleName))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.MiddleName);
        }


        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string middleName)
            {
                UpdateFacet(x => x.MiddleName = middleName);
            }
        }
       
    }
}