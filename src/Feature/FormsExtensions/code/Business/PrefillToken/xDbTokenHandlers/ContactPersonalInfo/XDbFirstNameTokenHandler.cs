using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbFirstNameTokenHandler : BaseXDbTokenHandler<IContactPersonalInfo>
    {
        private readonly IXDbService xDbService;

        public XDbFirstNameTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        protected override string GetFacetKey()
        {
            return CollectionModel.FacetKeys.PersonalInformation;
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(IContactPersonalInfo contactPersonalInfo)
        {
            if (string.IsNullOrEmpty(contactPersonalInfo.FirstName))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(contactPersonalInfo.FirstName);
        }

        public override void StoreTokenValue(object newValue)
        {
            if(newValue is string firstName) {
                xDbService.UpdateCurrentContactFacets(new ContactExpandOptions(GetFacetKey()),x=>x.Personal().FirstName=firstName);
            }
        }
    }
}