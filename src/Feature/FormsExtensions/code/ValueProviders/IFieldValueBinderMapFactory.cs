using Sitecore.ExperienceForms.Mvc.Models;

namespace Feature.FormsExtensions.ValueProviders
{
    public interface IFieldValueBinderMapFactory
    {
        IFieldValueBinder GetBindingHandler(ValueProviderSettings bindingSettingsValueProviderSettings);
    }
}
