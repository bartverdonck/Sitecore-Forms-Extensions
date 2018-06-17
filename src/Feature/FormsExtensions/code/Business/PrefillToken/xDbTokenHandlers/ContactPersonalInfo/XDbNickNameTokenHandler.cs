using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbNickNameTokenHandler : BaseXDbTokenHandler<IContactPersonalInfo> {
        private readonly IXDbService xDbService;

        public XDbNickNameTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        protected override string GetFacetKey()
        {
            return CollectionModel.FacetKeys.PersonalInformation;
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(IContactPersonalInfo contactPersonalInfo)
        {
            if (string.IsNullOrEmpty(contactPersonalInfo.Nickname))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(contactPersonalInfo.Nickname);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string nickName)
            {
                xDbService.UpdateCurrentContactFacets(new ContactExpandOptions(GetFacetKey()), x => x.Personal().Nickname = nickName);
            }
        }
    }
}