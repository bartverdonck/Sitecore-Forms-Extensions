using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbGenderTokenHandler : BaseXDbTokenHandler<IContactPersonalInfo>
    {
        private readonly IXDbService xDbService;

        public XDbGenderTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        protected override string GetFacetKey()
        {
            return CollectionModel.FacetKeys.PersonalInformation;
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(IContactPersonalInfo contactPersonalInfo)
        {
            if (string.IsNullOrEmpty(contactPersonalInfo.Gender))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(contactPersonalInfo.Gender);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string gender)
            {
                xDbService.UpdateCurrentContactFacets(new ContactExpandOptions(GetFacetKey()), x => x.Personal().Gender = gender);
            }
        }
    }
}