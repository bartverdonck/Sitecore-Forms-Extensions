using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public class XDbCityBindingHandler : PreferredAddressBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.City))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(addres.City);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string city)
            {
                UpdateFacet(x => x.PreferredAddress.City = city);
            }
        }

    }
}