using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbAddressLine2FieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine2))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.AddressLine2);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string addressLine2)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine2 = addressLine2);
            }
        }
    }
}