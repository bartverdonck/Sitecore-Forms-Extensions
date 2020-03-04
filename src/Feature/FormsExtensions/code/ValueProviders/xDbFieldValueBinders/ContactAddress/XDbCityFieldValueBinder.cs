using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbCityFieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.City))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.City);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string city)
            {
                UpdateFacet(x => x.PreferredAddress.City = city);
            }
            if (newValue is List<string> valueList)
            {
                UpdateFacet(x => x.PreferredAddress.City = valueList.FirstOrDefault());
            }
        }

    }
}