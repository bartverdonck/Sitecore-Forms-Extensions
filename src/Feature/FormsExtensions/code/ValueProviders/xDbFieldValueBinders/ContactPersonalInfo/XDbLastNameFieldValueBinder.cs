using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbLastNameFieldValueBinder : PersonalInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.LastName))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.LastName);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string lastName)
            {
                UpdateFacet(x => x.LastName = lastName);
            }
            if (newValue is List<string> lastNameList)
            {
                UpdateFacet(x => x.LastName = lastNameList.FirstOrDefault());
            }
        }
    }
}