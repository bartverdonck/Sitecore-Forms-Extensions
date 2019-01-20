using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactConcent
{
    public abstract class ConsentInformationBindingHandler : BaseXDbBindingHandler<ConsentInformation>
    {
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