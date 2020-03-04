using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbJobTitleFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.JobTitle))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.JobTitle);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string jobTitle)
            {
                UpdateFacet( x => x.JobTitle = jobTitle);
            }
            if (newValue is List<string> jobTitleList)
            {
                UpdateFacet(x => x.JobTitle = jobTitleList.FirstOrDefault());
            }
        }
        
    }
    
}