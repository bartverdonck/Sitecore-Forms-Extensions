using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbBirthDateBindingHandler : PersonalInformationBindingHandler
    {
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (!facet.Birthdate.HasValue)
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.Birthdate);
        }
        
        public override void StoreBindingValue(object newValue)
        {
            if (newValue is System.DateTime birthDate)
            {
                UpdateFacet(x=>x.Birthdate=birthDate);
            }
        }
        
    }
}