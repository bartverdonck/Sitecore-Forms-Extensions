using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers
{
    public class PreferredEmailTokenHandler : BaseXDbTokenHandler<EmailAddressList>
    {
        public PreferredEmailTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override string GetFacetKey()
        {
            return EmailAddressList.DefaultFacetKey;
        }

        protected override EmailAddressList CreateFacet()
        {
            return new EmailAddressList(new EmailAddress("",true), Sitecore.Configuration.Settings.GetSetting("XDbPreferredEmailAddress", "preferred"));
        }

        public override void StoreTokenValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredEmail.SmtpAddress = value);
            }
        }

        protected override ITokenHandlerResult GetTokenValueFromFacet(EmailAddressList facet)
        {
            if (facet.PreferredEmail == null || string.IsNullOrEmpty(facet.PreferredEmail.SmtpAddress))
            {
                return new NoTokenValueFoundResult();
            }

            return new TokenValueFoundResult(facet.PreferredEmail.SmtpAddress);
        }

    }

}