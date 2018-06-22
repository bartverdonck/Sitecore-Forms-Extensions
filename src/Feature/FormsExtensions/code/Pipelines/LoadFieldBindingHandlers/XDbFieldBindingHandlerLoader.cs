using Feature.FormsExtensions.Business.FieldBindings;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactConcent;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPhoneNumbers;
using Feature.FormsExtensions.XDb;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.LoadFieldBindingHandlers
{
    public class XDbFieldBindingHandlerLoader : MvcPipelineProcessor<LoadFieldBindingHandlersArgs>
    {
        private readonly IXDbService xDbService;
        

        public XDbFieldBindingHandlerLoader(IXDbService xDbService)
        {
            this.xDbService = xDbService;
        }

        public override void Process(LoadFieldBindingHandlersArgs args)
        {
            AddPersonalInfoHandlers(args);
            AddPreferedAddressHandlers(args);
            AddPreferedPhoneNumberHandlers(args);
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey("xDB Prefered Email", "sfe.xdb.email.smtp", "Email"), new PreferredEmailBindingHandler(xDbService));
            AddConsentInformationHandlers(args);
        }

        private void AddConsentInformationHandlers(LoadFieldBindingHandlersArgs args)
        {
            var category = "xDB Consent Information";
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.ci.revoked", "Consent Revoked"), new XDbConsentRevokedBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.ci.exrightforgot", "Execute Right To Be Forgotten"), new XDbExecutedRightToBeForgottenBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.ci.donotmarket", "Do Not Market"), new XDbDoNotMarketBindingHandler(xDbService));
        }

        private void AddPersonalInfoHandlers(LoadFieldBindingHandlersArgs args)
        {
            var category = "xDB Personal Info";
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.birthdate", "Birthdate"),
                new XDbBirthDateBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.firstName", "First Name"),
                new XDbFirstNameBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.gender", "Gender"),
                new XDbGenderBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.jobTitle", "Job Title"),
                new XDbJobTitleBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.middleName", "Middle Name"),
                new XDbMiddleNameBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.nickName", "Nick Name"),
                new XDbNickNameBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.suffix", "Suffix"),
                new XDbSuffixBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.lastName", "Last Name"),
                new XDbLastNameBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pi.title", "Title"),
                new XDbTitleBindingHandler(xDbService));
        }

        private void AddPreferedAddressHandlers(LoadFieldBindingHandlersArgs args)
        {
            var category = "xDB Prefered Address";
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.addressline1", "Address Line 1"), new XDbAddressLine1BindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.addressline2", "Address Line 2"), new XDbAddressLine2BindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.addressline3", "Address Line 3"), new XDbAddressLine3BindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.addressline4", "Address Line 4"), new XDbAddressLine4BindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.city", "Address City"), new XDbCityBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.country", "Address Country Code"), new Business.FieldBindings.xDbBindingHandlers.ContactAddress.XDbCountryCodeBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.postal", "Address Postal Code"), new XDbPostalCodeBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.padress.stateprov", "Address State Or Province"), new XDbStateOrProvinceBindingHandler(xDbService));
        }

        private void AddPreferedPhoneNumberHandlers(LoadFieldBindingHandlersArgs args)
        {
            var category = "xDB Prefered Phone Number";
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pphone.country", "Phone Number Country Code"), new Business.FieldBindings.xDbBindingHandlers.ContactPhoneNumbers.XDbCountryCodeBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pphone.number", "Phone Number"), new XDbNumberBindingHandler(xDbService));
            args.FieldBindingHandlers.Add(new FieldBindingTokenKey(category, "sfe.xdb.pphone.extension", "Phone Number Extension"), new XDbExtensionBindingHandler(xDbService));
        }

    }
}