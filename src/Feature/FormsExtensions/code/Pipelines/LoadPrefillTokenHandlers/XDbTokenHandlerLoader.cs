using Feature.FormsExtensions.Business.PrefillToken;
using Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers;
using Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress;
using Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactConcent;
using Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPersonalInfo;
using Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers;
using Feature.FormsExtensions.XDb;
using Sitecore.Mvc.Pipelines;
using XDbCountryCodeTokenHandler = Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress.XDbCountryCodeTokenHandler;

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
            AddPreferedAddressHandlers(args);
            AddPreferedPhoneNumberHandlers(args);
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.email.smtp", "xDb Profile: Email"), new PreferredEmailTokenHandler(xDbService));
            AddConsentInformationHandlers(args);
        }

        private void AddConsentInformationHandlers(LoadPrefillTokenHandlersArgs args)
        {
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.ci.revoked", "xDb Profile: Consent Revoked"), new XDbConsentRevokedTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.ci.exrightforgot", "xDb Profile: Execute Right To Be Forgotten"), new XDbExecutedRightToBeForgottenTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.ci.donotmarket", "xDb Profile: Do Not Market"), new XDbDoNotMarketTokenHandler(xDbService));
        }

        private void AddPersonalInfoHandlers(LoadPrefillTokenHandlersArgs args)
        {
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.birthdate", "xDb Profile: Birthdate"),
                new XDbLastNameTokenHandler(xDbService));
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
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.lastName", "xDb Profile: Last Name"),
                new XDbLastNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pi.title", "xDb Profile: Title"),
                new XDbTitleTokenHandler(xDbService));
        }

        private void AddPreferedAddressHandlers(LoadPrefillTokenHandlersArgs args)
        {
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.addressline1", "xDb Profile: Address Line 1"), new XDbAddressLine1TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.addressline2", "xDb Profile: Address Line 2"), new XDbAddressLine2TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.addressline3", "xDb Profile: Address Line 3"), new XDbAddressLine3TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.addressline4", "xDb Profile: Address Line 4"), new XDbAddressLine4TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.city", "xDb Profile: Address City"), new XDbCityTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.country", "xDb Profile: Address Country Code"), new XDbCountryCodeTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.postal", "xDb Profile: Address Postal Code"), new XDbPostalCodeTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.padress.stateprov", "xDb Profile: Address State Or Province"), new XDbStateOrProvinceTokenHandler(xDbService));
        }

        private void AddPreferedPhoneNumberHandlers(LoadPrefillTokenHandlersArgs args)
        {
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pphone.country", "xDb Profile: Phone Number Country Code"), new Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers.XDbCountryCodeTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pphone.number", "xDb Profile: Phone Number"), new XDbNumberTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey("sfe.xdb.pphone.extension", "xDb Profile: Phone Number Extension"), new XDbExtensionTokenHandler(xDbService));
        }

    }
}