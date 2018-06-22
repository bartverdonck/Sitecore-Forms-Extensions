using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbLastNameBindingHandler : PersonalInformationBindingHandler
    {
        public XDbLastNameBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.LastName))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.LastName);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string lastName)
            {
                UpdateFacet(x => x.LastName = lastName);
            }
        }
    }
}