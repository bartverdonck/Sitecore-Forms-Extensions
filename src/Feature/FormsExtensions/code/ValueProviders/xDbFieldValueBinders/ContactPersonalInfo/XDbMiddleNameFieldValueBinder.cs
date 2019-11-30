using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbMiddleNameFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.MiddleName))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.MiddleName);
        }


        public override void StoreValue(object newValue)
        {
            if (newValue is string middleName)
            {
                UpdateFacet(x => x.MiddleName = middleName);
            }
        }
       
    }
}