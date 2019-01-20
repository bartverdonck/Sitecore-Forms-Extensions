using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPhoneNumbers
{
    public class XDbCountryCodeBindingHandler : PreferredPhoneNumbersBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.CountryCode))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(phoneNumber.CountryCode);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.CountryCode = value);
            }
        }

    }
}