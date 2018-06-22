using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactConcent
{
    public class XDbDoNotMarketBindingHandler : ConsentInformationBindingHandler
    {
        public XDbDoNotMarketBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(ConsentInformation facet)
        {
            return new BindingValueFoundResult(facet.DoNotMarket);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x => x.DoNotMarket = value);
            }
        }
    }
}