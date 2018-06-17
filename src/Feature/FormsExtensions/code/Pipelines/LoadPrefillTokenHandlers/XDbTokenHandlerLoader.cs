using Feature.FormsExtensions.Business.PrefillToken;
using Feature.FormsExtensions.XDb;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.LoadPrefillTokenHandlers
{
    public class XDbTokenHandlerLoader : MvcPipelineProcessor<LoadPrefillTokenHandlersArgs>
    {
        private readonly IXDbService xDbService;

        public XDbTokenHandlerLoader(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        public override void Process(LoadPrefillTokenHandlersArgs args)
        {
            args.TokenHandlers.Add("token.test", new XDbFirstNameTokenHandler());
            args.TokenHandlers.Add("token.test2", new XDbFirstNameTokenHandler());
        }
    }

    public class XDbFirstNameTokenHandler : IPrefillTokenHandler {

        public ITokenHandlerResult GetTokenValue()
        {
            if (!Tracker.Enabled)
                return new NoTokenValueFoundResult();
            if (!Tracker.IsActive)
                return new NoTokenValueFoundResult();
            if (Tracker.Current?.Contact == null)
                return new NoTokenValueFoundResult();
            if (Tracker.Current.Contact.IdentificationLevel != ContactIdentificationLevel.Known)
                return new NoTokenValueFoundResult();
            var personalContactFacet = Tracker.Current.Contact.Facets["Personal"] as IContactPersonalInfo;
            if (personalContactFacet == null)
                return new NoTokenValueFoundResult();
            return new TokenValueFoundResult(personalContactFacet.FirstName);
        }

        public void StoreTokenValue(object newValue)
        {
            throw new System.NotImplementedException();
        }

    }
}