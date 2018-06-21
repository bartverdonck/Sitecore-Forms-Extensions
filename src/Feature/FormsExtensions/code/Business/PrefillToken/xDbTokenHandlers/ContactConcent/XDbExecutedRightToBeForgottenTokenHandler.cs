using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactConcent
{
    public class XDbExecutedRightToBeForgottenTokenHandler : ConsentInformationTokenHandler
    {
        public XDbExecutedRightToBeForgottenTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(ConsentInformation facet)
        {
            return new TokenValueFoundResult(facet.ExecutedRightToBeForgotten);
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is bool value)
            {
                UpdateFacet(x => x.ExecutedRightToBeForgotten = value);
            }
        }
    }
}