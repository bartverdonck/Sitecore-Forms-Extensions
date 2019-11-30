using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbAddressLine4FieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine4))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.AddressLine4);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string addressLine4)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine4 = addressLine4);
            }
        }
    }
}