using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public abstract class PersonalInformationFieldValueBinder : BaseXDbFieldValueBinder<PersonalInformation>
    {
        
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