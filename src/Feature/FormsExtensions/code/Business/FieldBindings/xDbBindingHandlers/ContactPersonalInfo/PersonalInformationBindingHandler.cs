using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public abstract class PersonalInformationBindingHandler : BaseXDbBindingHandler<PersonalInformation>
    {
        protected PersonalInformationBindingHandler(IXDbService xDbService) : base(xDbService)
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