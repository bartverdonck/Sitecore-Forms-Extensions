using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbBirthDateBindingHandler : PersonalInformationBindingHandler
    {
        public XDbBirthDateBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (facet.Birthdate.HasValue)
                return new NoBindingValueFoundResult();
            // ReSharper disable once PossibleInvalidOperationException
            return new BindingValueFoundResult(facet.Birthdate.Value);
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