using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders
{
    public class PreferredEmailFieldValueBinder : BaseXDbFieldValueBinder<EmailAddressList>
    {

        protected override string GetFacetKey()
        {
            return EmailAddressList.DefaultFacetKey;
        }

        protected override EmailAddressList CreateFacet()
        {
            return new EmailAddressList(new EmailAddress("",true), Sitecore.Configuration.Settings.GetSetting("XDbPreferredEmailAddress", "preferred"));
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string value)
            {
                UpdateFacet(x => x.PreferredEmail.SmtpAddress = value);
            }
        }

        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(EmailAddressList facet)
        {
            if (string.IsNullOrEmpty(facet.PreferredEmail?.SmtpAddress))
            {
                return new NoFieldValueBindingFoundResult();
            }

            return new FieldValueBindingFoundResult(facet.PreferredEmail.SmtpAddress);
        }

    }

}