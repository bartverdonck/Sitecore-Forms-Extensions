using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbFirstNameBindingHandler : PersonalInformationBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.FirstName))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.FirstName);
        }

        public override void StoreBindingValue(object newValue)
        {
            if(newValue is string firstName) {
                UpdateFacet(x=>x.FirstName=firstName);
            }
        }
    }
}