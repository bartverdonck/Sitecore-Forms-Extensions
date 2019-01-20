using Sitecore.ExperienceForms.ValueProviders;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public interface IBindingHandler : IFieldValueProvider
    {
        void StoreBindingValue(object newValue);
    }
}