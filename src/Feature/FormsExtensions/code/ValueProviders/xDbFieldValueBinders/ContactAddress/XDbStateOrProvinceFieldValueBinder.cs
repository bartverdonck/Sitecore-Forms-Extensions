using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbStateOrProvinceFieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.StateOrProvince))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.StateOrProvince);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.StateOrProvince = value);
            }
            if (newValue is List<string> valueList)
            {
                UpdateFacet(x => x.PreferredAddress.StateOrProvince = valueList.FirstOrDefault());
            }
        }

    }
}