using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo
{
    public abstract class PersonalInformationTokenHandler : BaseXDbTokenHandler<PersonalInformation>
    {
        protected PersonalInformationTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override string GetFacetKey()
        {
            return PersonalInformation.DefaultFacetKey;
        }

        protected override PersonalInformation CreateFacet()
        {
            return new PersonalInformation();
        }
    }
}