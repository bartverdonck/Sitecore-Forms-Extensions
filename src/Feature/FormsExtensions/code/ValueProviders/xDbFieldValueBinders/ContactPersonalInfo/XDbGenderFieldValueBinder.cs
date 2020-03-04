using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbGenderFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Gender))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.Gender);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string gender)
            {
                UpdateFacet(x=>x.Gender=gender);
            }

            if (newValue is List<string> genderList)
            {
                UpdateFacet(x => x.Gender = genderList.FirstOrDefault());
            }
        }
        
    }
}