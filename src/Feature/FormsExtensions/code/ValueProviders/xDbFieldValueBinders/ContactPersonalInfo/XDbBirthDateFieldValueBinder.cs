using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbBirthDateFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (!facet.Birthdate.HasValue)
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.Birthdate);
        }
        
        public override void StoreValue(object newValue)
        {
            if (newValue is System.DateTime birthDate)
            {
                UpdateFacet(x=>x.Birthdate=birthDate);
            }
        }
        
    }
}