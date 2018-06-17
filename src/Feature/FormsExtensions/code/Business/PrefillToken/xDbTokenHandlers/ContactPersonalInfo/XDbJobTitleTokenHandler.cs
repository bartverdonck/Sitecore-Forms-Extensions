using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbJobTitleTokenHandler : BaseXDbTokenHandler<IContactPersonalInfo>
    {
        private readonly IXDbService xDbService;

        public XDbJobTitleTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        protected override string GetFacetKey()
        {
            return CollectionModel.FacetKeys.PersonalInformation;
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(IContactPersonalInfo contactPersonalInfo)
        {
            if (string.IsNullOrEmpty(contactPersonalInfo.JobTitle))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(contactPersonalInfo.JobTitle);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string jobTitle)
            {
                xDbService.UpdateCurrentContactFacets(new ContactExpandOptions(GetFacetKey()), x => x.Personal().JobTitle = jobTitle);
            }
        }
    }
}