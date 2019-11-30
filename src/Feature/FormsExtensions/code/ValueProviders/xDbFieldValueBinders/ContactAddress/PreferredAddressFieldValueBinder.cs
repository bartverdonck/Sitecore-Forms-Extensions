using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactAddress
{
    public abstract class PreferredAddressFieldValueBinder : BaseXDbFieldValueBinder<AddressList>
    {
        protected override string GetFacetKey()
        {
            return AddressList.DefaultFacetKey;
        }

        protected override AddressList CreateFacet()
        {
            return new AddressList(new Address(), Sitecore.Configuration.Settings.GetSetting("XDbPreferredAddress", "preferred"));
        }
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(AddressList facet)
        {
            if (facet.PreferredAddress == null)
            {
                return new NoFieldValueBindingFoundResult();
            }
            return GetFieldBindingValueFromFacet(facet.PreferredAddress);
        }

        protected abstract IFieldValueBinderResult GetFieldBindingValueFromFacet(Address preferredAddress);

    }

}