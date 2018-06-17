using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbSuffixTokenHandler : BaseXDbTokenHandler<IContactPersonalInfo>
    {
        private readonly IXDbService xDbService;

        public XDbSuffixTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        protected override string GetFacetKey()
        {
            return CollectionModel.FacetKeys.PersonalInformation;
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(IContactPersonalInfo contactPersonalInfo)
        {
            if (string.IsNullOrEmpty(contactPersonalInfo.Suffix))
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(contactPersonalInfo.Suffix);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string suffix)
            {
                xDbService.UpdateCurrentContactFacets(new ContactExpandOptions(GetFacetKey()), x => x.Personal().Suffix = suffix);
            }
        }
    }
}