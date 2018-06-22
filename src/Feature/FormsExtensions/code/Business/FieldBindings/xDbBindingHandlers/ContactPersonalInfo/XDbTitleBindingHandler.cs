using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbTitleBindingHandler : PersonalInformationBindingHandler
    {
        public XDbTitleBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Title))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.Title);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string title)
            {
                UpdateFacet( x => x.Title = title);
            }
        }
        
    }
}