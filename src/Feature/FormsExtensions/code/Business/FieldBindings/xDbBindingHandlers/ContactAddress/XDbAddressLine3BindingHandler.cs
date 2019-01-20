using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public class XDbAddressLine3BindingHandler : PreferredAddressBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine3))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(addres.AddressLine3);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string addressLine3)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine3 = addressLine3);
            }
        }
    }
}