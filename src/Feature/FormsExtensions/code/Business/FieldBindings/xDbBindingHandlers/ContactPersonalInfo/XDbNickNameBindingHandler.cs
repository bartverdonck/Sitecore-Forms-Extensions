using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo
{
    public class XDbNickNameBindingHandler : PersonalInformationBindingHandler {

        public XDbNickNameBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Nickname))
                return new NoBindingValueFoundResult();
            return new BindingValueFoundResult(facet.Nickname);
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string nickName)
            {
               UpdateFacet(x => x.Nickname = nickName);
            }
        }
        
    }
}