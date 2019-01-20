using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers
{
    public class PreferredEmailBindingHandler : BaseXDbBindingHandler<EmailAddressList>
    {

        protected override string GetFacetKey()
        {
            return EmailAddressList.DefaultFacetKey;
        }

        protected override EmailAddressList CreateFacet()
        {
            return new EmailAddressList(new EmailAddress("",true), Sitecore.Configuration.Settings.GetSetting("XDbPreferredEmailAddress", "preferred"));
        }

        public override void StoreBindingValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredEmail.SmtpAddress = value);
            }
        }

        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(EmailAddressList facet)
        {
            if (string.IsNullOrEmpty(facet.PreferredEmail?.SmtpAddress))
            {
                return new NoBindingValueFoundResult();
            }

            return new BindingValueFoundResult(facet.PreferredEmail.SmtpAddress);
        }

    }

}