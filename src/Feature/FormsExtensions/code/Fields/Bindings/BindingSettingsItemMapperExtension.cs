using Sitecore;
using Sitecore.Data.Items;

namespace Feature.FormsExtensions.Fields.Bindings
{
    public static class BindingSettingsItemMapperExtension
    {
        private const string StoreBindingValueParam = "Store Binding Value";

        public static void InitBindingSettingsProperties(this IBindingSettings bindingSettings, Item item)
        {
            bindingSettings.StoreBindingValue = MainUtil.GetBool(item.Fields[StoreBindingValueParam]?.Value, false);
        }

        public static void UpdateBindingSettingsFields(this IBindingSettings bindingSettings, Item item)
        {
            item.Fields[StoreBindingValueParam]?.SetValue(bindingSettings.StoreBindingValue ? "1" : string.Empty, true);
        }
    }
}