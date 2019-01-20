using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactConcent
{
    public class XDbExecutedRightToBeForgottenBindingHandler : ConsentInformationBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(ConsentInformation facet)
        {
            return new BindingValueFoundResult(facet.ExecutedRightToBeForgotten);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x => x.ExecutedRightToBeForgotten = value);
            }
        }
    }
}