using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPhoneNumbers
{
    public abstract class PreferredPhoneNumbersFieldValueBinder : BaseXDbFieldValueBinder<PhoneNumberList>
    {
        protected override string GetFacetKey()
        {
            return PhoneNumberList.DefaultFacetKey;
        }

        protected override PhoneNumberList CreateFacet()
        {
            return new PhoneNumberList(new PhoneNumber("",""), Sitecore.Configuration.Settings.GetSetting("XDbPreferredPhoneNumber", "preferred"));
        }
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PhoneNumberList facet)
        {
            if (facet.PreferredPhoneNumber == null)
            {
                return new NoFieldValueBindingFoundResult();
            }
            return GetFieldBindingValueFromFacet(facet.PreferredPhoneNumber);
        }

        protected abstract IFieldValueBinderResult GetFieldBindingValueFromFacet(PhoneNumber phoneNumber);

    }
}