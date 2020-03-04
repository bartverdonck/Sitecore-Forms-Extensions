using System.Collections.Generic;
using System.Linq;
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
            if (newValue is List<string> titleList)
            {
                UpdateFacet(x => x.Title = titleList.FirstOrDefault());
            }
        }
        
    }
}