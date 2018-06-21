using Feature.FormsExtensions.Fields.Prefill;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.Bindings
{
    public class StringInputViewWithBindingsTokenModel : StringInputViewModel, IBindingSettings
    {
        public string BindingToken { get; set; }
        public bool PrefillBindingValue { get; set; }
        public bool StoreBindingValue { get; set; }

        private const string BindingTokenParam = "Binding Token";
        private const string PrefillBindingValueParam = "Prefill Binding Value";
        private const string StoreBindingValueParam = "Store Binding Value";

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            BindingToken = StringUtil.GetString(item.Fields[BindingTokenParam]);
            PrefillBindingValue = MainUtil.GetBool(item.Fields[PrefillBindingValueParam]?.Value, false);
            StoreBindingValue = MainUtil.GetBool(item.Fields[StoreBindingValueParam]?.Value,false);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            item.Fields[BindingTokenParam]?.SetValue(BindingToken, true);
            item.Fields[PrefillBindingValueParam]?.SetValue(PrefillBindingValue ? "1" : string.Empty,true);
            item.Fields[StoreBindingValueParam]?.SetValue(StoreBindingValue ? "1" : string.Empty, true);
        }
        
    }
}