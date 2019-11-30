using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactConcent
{
    public class XDbExecutedRightToBeForgottenFieldValueBinder : ConsentInformationFieldValueBinder
    {
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(ConsentInformation facet)
        {
            return new FieldValueBindingFoundResult(facet.ExecutedRightToBeForgotten);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x => x.ExecutedRightToBeForgotten = value);
            }
        }
    }
}