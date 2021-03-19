using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.Bindings
{
    [Serializable]
    public class CheckBoxListWithBindingsViewModel : CheckBoxListViewModel, IBindingSettings
    {
        public bool StoreBindingValue { get; set; }

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            this.InitBindingSettingsProperties(item);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            this.UpdateBindingSettingsFields(item);
        }

        protected override void InitializeValue(object value)
        {
            switch (value)
            {
                case null:
                    return;
                case string s:
                    Value = new List<string> { s };
                    break;
                case List<string> ls:
                    Value = ls;
                    break;
            }
        }
    }
}