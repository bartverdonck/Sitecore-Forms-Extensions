using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbGenderBindingHandler : PersonalInformationBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Gender))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.Gender);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string gender)
            {
                UpdateFacet(x=>x.Gender=gender);
            }
        }
        
    }
}