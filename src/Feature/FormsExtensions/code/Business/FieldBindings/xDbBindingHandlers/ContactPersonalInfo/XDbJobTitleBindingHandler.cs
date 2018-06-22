using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbJobTitleBindingHandler : PersonalInformationBindingHandler
    {

        public XDbJobTitleBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.JobTitle))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.JobTitle);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string jobTitle)
            {
                UpdateFacet( x => x.JobTitle = jobTitle);
            }
        }
        
    }
    
}