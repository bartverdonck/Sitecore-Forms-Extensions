using Sitecore.ExperienceForms.ValueProviders;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public interface IBindingHandler : IFieldValueProvider
    {
        IBindingHandlerResult GetBindingValue();
        void StoreBindingValue(object newValue);
    }
}