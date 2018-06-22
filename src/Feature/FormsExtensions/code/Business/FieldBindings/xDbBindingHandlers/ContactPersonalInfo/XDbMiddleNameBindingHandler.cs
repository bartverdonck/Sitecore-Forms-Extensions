using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbMiddleNameBindingHandler : PersonalInformationBindingHandler
    {
        public XDbMiddleNameBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.MiddleName))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.MiddleName);
        }


        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string middleName)
            {
                UpdateFacet(x => x.MiddleName = middleName);
            }
        }
       
    }
}