using Feature.FormsExtensions.XDb;
using Sitecore.Analytics.Model.Entities;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbBirthDateTokenHandler : BaseXDbTokenHandler<IContactPersonalInfo>
    {
        private readonly IXDbService xDbService;

        public XDbBirthDateTokenHandler(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        protected override string GetFacetKey()
        {
            return CollectionModel.FacetKeys.PersonalInformation;
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(IContactPersonalInfo contactPersonalInfo)
        {
            if (contactPersonalInfo.BirthDate.HasValue)
                return new NoTokenValueFoundResult();
            // ReSharper disable once PossibleInvalidOperationException
            return new TokenValueFoundResult(contactPersonalInfo.BirthDate.Value);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is System.DateTime birthDate)
            {
                xDbService.UpdateCurrentContactFacets(new ContactExpandOptions(GetFacetKey()), x => x.Personal().Birthdate = birthDate);
            }
        }
    }
}