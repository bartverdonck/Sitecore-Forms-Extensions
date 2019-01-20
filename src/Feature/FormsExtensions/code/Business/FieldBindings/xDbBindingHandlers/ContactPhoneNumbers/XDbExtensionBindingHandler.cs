using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPhoneNumbers
{
    public class XDbExtensionBindingHandler : PreferredPhoneNumbersBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.Extension))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(phoneNumber.Extension);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.Extension = value);
            }
        }

    }
}