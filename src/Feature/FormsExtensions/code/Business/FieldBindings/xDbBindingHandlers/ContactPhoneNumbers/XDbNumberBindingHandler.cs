using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPhoneNumbers
{
    public class XDbNumberBindingHandler : PreferredPhoneNumbersBindingHandler {
        public XDbNumberBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }


        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PhoneNumber phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber.Number))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(phoneNumber.Number);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredPhoneNumber.Number = value);
            }
        }
        
    }
}