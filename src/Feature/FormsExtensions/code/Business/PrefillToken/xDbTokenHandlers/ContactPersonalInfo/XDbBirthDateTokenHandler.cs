using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public class XDbBirthDateTokenHandler : PersonalInformationTokenHandler
    {
        public XDbBirthDateTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override ITokenHandlerResult GetTokenValueFromFacet(PersonalInformation facet)
        {
            if (facet.Birthdate.HasValue)
                return new NoTokenValueFoundResult();
            // ReSharper disable once PossibleInvalidOperationException
            return new TokenValueFoundResult(facet.Birthdate.Value);
        }
        
        public override void StoreTokenValue(object newValue)
        {
            if (newValue is System.DateTime birthDate)
            {
                UpdateFacet(x=>x.Birthdate=birthDate);
            }
        }
        
    }
}