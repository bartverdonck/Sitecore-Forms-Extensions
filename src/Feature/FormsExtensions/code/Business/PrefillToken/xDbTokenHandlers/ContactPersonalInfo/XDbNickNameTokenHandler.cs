using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbNickNameTokenHandler : PersonalInformationTokenHandler {

        public XDbNickNameTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Nickname))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(facet.Nickname);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string nickName)
            {
               UpdateFacet(x => x.Nickname = nickName);
            }
        }
        
    }
}