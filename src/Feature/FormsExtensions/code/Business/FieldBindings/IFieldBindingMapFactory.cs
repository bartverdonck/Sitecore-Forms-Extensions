using Sitecore.ExperienceForms.Mvc.Models;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public interface IFieldBindingMapFactory
    {
        IBindingHandler GetBindingHandler(ValueProviderSettings bindingSettingsValueProviderSettings);
    }
}
