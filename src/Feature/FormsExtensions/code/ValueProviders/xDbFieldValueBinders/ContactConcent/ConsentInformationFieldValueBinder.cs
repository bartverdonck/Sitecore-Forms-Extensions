using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactConcent
{
    public abstract class ConsentInformationFieldValueBinder : BaseXDbFieldValueBinder<ConsentInformation>
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