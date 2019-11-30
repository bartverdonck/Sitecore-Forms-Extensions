using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPhoneNumbers
{
    public class XDbCountryCodeFieldValueBinder : PreferredPhoneNumbersFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.CountryCode))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(phoneNumber.CountryCode);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.CountryCode = value);
            }
        }

    }
}