using System;
using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactAddress
{
    public abstract class PreferredAddressTokenHandler : BaseXDbTokenHandler<AddressList>
    {
        protected PreferredAddressTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override string GetFacetKey()
        {
            return AddressList.DefaultFacetKey;
        }

        protected override AddressList CreateFacet()
        {
            return new AddressList(new Address(), Sitecore.Configuration.Settings.GetSetting("XDbPreferedAddressList", "preferred"));
        }
        protected override ITokenHandlerResult GetTokenValueFromFacet(AddressList facet)
        {
            if (facet.PreferredAddress == null)
            {
                return new NoTokenValueFoundResult();
            }
            return GetTokenValueFromFacet(facet.PreferredAddress);
        }

        protected abstract ITokenHandlerResult GetTokenValueFromFacet(Address preferredAddress);

    }

}