using System;
using Feature.FormsExtensions.Fields.Bindings;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.Hidden
{
    [Serializable]
    public class HiddenViewModel : InputViewModel<string>, IBindingSettings
    {
        public bool StoreBindingValue { get; set; }

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            this.InitBindingSettingsProperties(item);
            Value = StringUtil.GetString(item.Fields["Default Value"]);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            this.UpdateBindingSettingsFields(item);
            item.Fields["Default Value"]?.SetValue(Value, true);
        }

        protected override void InitializeValue(object value)
        {
            Value = value?.ToString();
        }
    }
}