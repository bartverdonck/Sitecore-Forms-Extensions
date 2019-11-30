using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbFirstNameFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.FirstName))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.FirstName);
        }

        public override void StoreValue(object newValue)
        {
            if(newValue is string firstName) {
                UpdateFacet(x=>x.FirstName=firstName);
            }
        }
    }
}