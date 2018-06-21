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
            args.TokenHandlers.Add(new PrefillTokenKey("xDB Prefered Email", "sfe.xdb.email.smtp", "Email"), new PreferredEmailTokenHandler(xDbService));
            AddConsentInformationHandlers(args);
        }

        private void AddConsentInformationHandlers(LoadPrefillTokenHandlersArgs args)
        {
            var tokenCategory = "xDB Consent Information";
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.ci.revoked", "Consent Revoked"), new XDbConsentRevokedTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.ci.exrightforgot", "Execute Right To Be Forgotten"), new XDbExecutedRightToBeForgottenTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.ci.donotmarket", "Do Not Market"), new XDbDoNotMarketTokenHandler(xDbService));
        }

        private void AddPersonalInfoHandlers(LoadPrefillTokenHandlersArgs args)
        {
            var tokenCategory = "xDB Personal Info";
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.birthdate", "Birthdate"),
                new XDbLastNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.firstName", "First Name"),
                new XDbFirstNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.gender", "Gender"),
                new XDbGenderTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.jobTitle", "Job Title"),
                new XDbJobTitleTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.middleName", "Middle Name"),
                new XDbMiddleNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.nickName", "Nick Name"),
                new XDbNickNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.suffix", "Suffix"),
                new XDbSuffixTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.lastName", "Last Name"),
                new XDbLastNameTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pi.title", "Title"),
                new XDbTitleTokenHandler(xDbService));
        }

        private void AddPreferedAddressHandlers(LoadPrefillTokenHandlersArgs args)
        {
            var tokenCategory = "xDB Prefered Address";
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.addressline1", "Address Line 1"), new XDbAddressLine1TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.addressline2", "Address Line 2"), new XDbAddressLine2TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.addressline3", "Address Line 3"), new XDbAddressLine3TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.addressline4", "Address Line 4"), new XDbAddressLine4TokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.city", "Address City"), new XDbCityTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.country", "Address Country Code"), new XDbCountryCodeTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.postal", "Address Postal Code"), new XDbPostalCodeTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.padress.stateprov", "Address State Or Province"), new XDbStateOrProvinceTokenHandler(xDbService));
        }

        private void AddPreferedPhoneNumberHandlers(LoadPrefillTokenHandlersArgs args)
        {
            var tokenCategory = "xDB Prefered Phone Number";
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pphone.country", "Phone Number Country Code"), new Business.PrefillToken.xDbTokenHandlers.ContactPhoneNumbers.XDbCountryCodeTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pphone.number", "Phone Number"), new XDbNumberTokenHandler(xDbService));
            args.TokenHandlers.Add(new PrefillTokenKey(tokenCategory, "sfe.xdb.pphone.extension", "Phone Number Extension"), new XDbExtensionTokenHandler(xDbService));
        }

    }
}