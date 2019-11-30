using Sitecore.ExperienceForms.ValueProviders;

namespace Feature.FormsExtensions.ValueProviders
{
    public interface IFieldValueBinder : IFieldValueProvider
    {
        void StoreValue(object newValue);
    }
}