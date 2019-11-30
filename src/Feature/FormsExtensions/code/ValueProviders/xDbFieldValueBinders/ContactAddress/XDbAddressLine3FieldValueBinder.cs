using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public class XDbAddressLine3FieldValueBinder : PreferredAddressFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine3))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(addres.AddressLine3);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string addressLine3)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine3 = addressLine3);
            }
        }
    }
}