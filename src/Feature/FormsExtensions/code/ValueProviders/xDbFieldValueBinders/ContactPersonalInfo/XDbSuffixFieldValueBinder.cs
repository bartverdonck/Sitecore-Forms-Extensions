using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbSuffixFieldValueBinder : PersonalInformationFieldValueBinder
    {
        
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Suffix))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.Suffix);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string suffix)
            {
                UpdateFacet(x => x.Suffix = suffix);
            }
            if (newValue is List<string> suffixList)
            {
                UpdateFacet(x => x.Suffix = suffixList.FirstOrDefault());
            }
        }
       
    }
}