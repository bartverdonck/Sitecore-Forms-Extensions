using Feature.FormsExtensions.Fields.Bindings;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.Hidden
{
    public class HiddenViewModel : InputViewModel<string>, IBindingSettings
    {
        public bool PrefillBindingValue { get; set; }
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
    }
}