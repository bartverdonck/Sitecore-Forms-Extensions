using Sitecore;
using Sitecore.Data.Items;

namespace Feature.FormsExtensions.Fields.Bindings
{
    public static class BindingSettingsItemMapperExtension
    {
        private const string BindingTokenParam = "Binding Token";
        private const string PrefillBindingValueParam = "Prefill Binding Value";
        private const string StoreBindingValueParam = "Store Binding Value";

        public static void InitBindingSettingsProperties(this IBindingSettings bindingSettings, Item item)
        {
            bindingSettings.BindingToken = StringUtil.GetString(item.Fields[BindingTokenParam]);
            bindingSettings.PrefillBindingValue = MainUtil.GetBool(item.Fields[PrefillBindingValueParam]?.Value, false);
            bindingSettings.StoreBindingValue = MainUtil.GetBool(item.Fields[StoreBindingValueParam]?.Value, false);
        }

        public static void UpdateBindingSettingsFields(this IBindingSettings bindingSettings, Item item)
        {
            item.Fields[BindingTokenParam]?.SetValue(bindingSettings.BindingToken, true);
            item.Fields[PrefillBindingValueParam]?.SetValue(bindingSettings.PrefillBindingValue ? "1" : string.Empty, true);
            item.Fields[StoreBindingValueParam]?.SetValue(bindingSettings.StoreBindingValue ? "1" : string.Empty, true);
        }
    }
}