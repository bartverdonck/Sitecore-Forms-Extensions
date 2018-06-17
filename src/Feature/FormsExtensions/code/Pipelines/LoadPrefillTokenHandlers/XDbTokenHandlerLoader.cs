using Feature.FormsExtensions.Business.PrefillToken;
using Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo;
using Feature.FormsExtensions.XDb;
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
            AddPersonalInfoHandlers(args);
        }

        private void AddPersonalInfoHandlers(LoadPrefillTokenHandlersArgs args)
        {
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.birthdate", "xDb Profile: Birthdate"),
                new XDbSurNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.firstName", "xDb Profile: First Name"),
                new XDbFirstNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.gender", "xDb Profile: Gender"),
                new XDbGenderTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.jobTitle", "xDb Profile: Job Title"),
                new XDbJobTitleTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.middleName", "xDb Profile: Middle Name"),
                new XDbMiddleNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.nickName", "xDb Profile: Nick Name"),
                new XDbNickNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.suffix", "xDb Profile: Suffix"),
                new XDbSuffixTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.surname", "xDb Profile: Surname"),
                new XDbSurNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.title", "xDb Profile: Title"),
                new XDbTitleTokenHandler(xDbService));
        }
    }
}