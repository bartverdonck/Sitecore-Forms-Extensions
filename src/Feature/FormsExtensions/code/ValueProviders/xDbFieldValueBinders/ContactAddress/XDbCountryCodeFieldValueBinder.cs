using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbCountryCodeFieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.CountryCode))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.CountryCode);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.CountryCode = value);
            }
        }

    }
}