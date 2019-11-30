using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbTitleFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Title))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.Title);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string title)
            {
                UpdateFacet( x => x.Title = title);
            }
        }
        
    }
}