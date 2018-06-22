using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public class XDbCountryCodeBindingHandler : PreferredAddressBindingHandler
    {
        public XDbCountryCodeBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.CountryCode))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(addres.CountryCode);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.CountryCode = value);
            }
        }

    }
}