using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPhoneNumbers
{
    public class XDbExtensionFieldValueBinder : PreferredPhoneNumbersFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.Extension))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(phoneNumber.Extension);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.Extension = value);
            }
        }

    }
}