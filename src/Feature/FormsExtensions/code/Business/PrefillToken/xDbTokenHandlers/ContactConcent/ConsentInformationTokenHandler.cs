using Feature.FormsExtensions.XDb;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.PrefillToken.xDbTokenHandlers.ContactConcent
{
    public abstract class ConsentInformationTokenHandler : BaseXDbTokenHandler<ConsentInformation>
    {
        protected ConsentInformationTokenHandler(IXDbService xDbService) : base(xDbService)
        {
        }

        protected override string GetFacetKey()
        {
            return ConsentInformation.DefaultFacetKey;
        }

        protected override ConsentInformation CreateFacet()
        {
            return new ConsentInformation();
        }
    }
}