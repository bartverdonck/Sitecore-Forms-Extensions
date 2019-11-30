using Sitecore.ExperienceForms.Mvc.Models;

namespace Feature.FormsExtensions.Fields.Bindings
{
    public interface IBindingSettings
    {
        ValueProviderSettings ValueProviderSettings { get; set; }
        bool StoreBindingValue { get; set; }
    }
}