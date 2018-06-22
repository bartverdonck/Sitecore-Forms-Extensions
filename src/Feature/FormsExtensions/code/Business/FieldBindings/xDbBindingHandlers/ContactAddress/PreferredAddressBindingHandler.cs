using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress
{
    public abstract class PreferredAddressBindingHandler : BaseXDbBindingHandler<AddressList>
    {
        protected PreferredAddressBindingHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override string GetFacetKey()
        {
            return AddressList.DefaultFacetKey;
        }

        protected override AddressList CreateFacet()
        {
            return new AddressList(new Address(), Sitecore.Configuration.Settings.GetSetting("XDbPreferredAddress", "preferred"));
        }
        protected override IBindingHandlerResult GetFieldBindingValueFromFacet(AddressList facet)
        {
            if (facet.PreferredAddress == null)
            {
                return new NoBindingValueFoundResult();
            }
            return GetFieldBindingValueFromFacet(facet.PreferredAddress);
        }

        protected abstract IBindingHandlerResult GetFieldBindingValueFromFacet(Address preferredAddress);

    }

}