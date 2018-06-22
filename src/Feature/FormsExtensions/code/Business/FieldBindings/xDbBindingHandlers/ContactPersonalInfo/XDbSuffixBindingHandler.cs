using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbSuffixBindingHandler : PersonalInformationBindingHandler
    {
        public XDbSuffixBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }
        
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Suffix))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.Suffix);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string suffix)
            {
                UpdateFacet(x => x.Suffix = suffix);
            }
        }
       
    }
}