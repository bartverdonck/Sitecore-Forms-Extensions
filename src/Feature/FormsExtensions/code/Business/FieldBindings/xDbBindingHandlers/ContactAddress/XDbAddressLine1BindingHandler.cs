using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public class XDbAddressLine1BindingHandler: PreferredAddressBindingHandler
    {
        public XDbAddressLine1BindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.AddressLine1))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(addres.AddressLine1);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string addressLine1)
            {
                UpdateFacet(x => x.PreferredAddress.AddressLine1=addressLine1);
            }
        }

    }
}