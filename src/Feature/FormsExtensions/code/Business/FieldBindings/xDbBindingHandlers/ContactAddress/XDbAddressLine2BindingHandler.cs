using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public class XDbAddressLine2BindingHandler : PreferredAddressBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine2))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(addres.AddressLine2);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string addressLine2)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine2 = addressLine2);
            }
        }
    }
}