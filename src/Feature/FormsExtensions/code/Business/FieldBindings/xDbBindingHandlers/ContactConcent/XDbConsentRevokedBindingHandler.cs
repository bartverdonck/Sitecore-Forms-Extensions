using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactConcent
{
    public class XDbConsentRevokedBindingHandler : ConsentInformationBindingHandler
    {
        public XDbConsentRevokedBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(ConsentInformation facet)
        {
            return new BindingValueFoundResult(facet.ConsentRevoked);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x=>x.ConsentRevoked=value);
            }
        }
    }
}