using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public class XDbStateOrProvinceBindingHandler : PreferredAddressBindingHandler
    {
        public XDbStateOrProvinceBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(Address addres)
        {
            if (string.IsNullOrEmpty(addres.StateOrProvince))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(addres.StateOrProvince);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredAddress.StateOrProvince = value);
            }
        }

    }
}