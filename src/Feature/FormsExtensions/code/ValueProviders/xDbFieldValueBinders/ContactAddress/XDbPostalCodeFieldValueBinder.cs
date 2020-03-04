using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbPostalCodeFieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.PostalCode))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.PostalCode);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.PostalCode = value);
            }
            if (newValue is List<string> valueList)
            {
                UpdateFacet(x => x.PreferredAddress.PostalCode = valueList.FirstOrDefault());
            }
        }

    }
}