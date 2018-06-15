using Sitecore;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.Prefill
{
    public class StringInputViewWithPrefillTokenModel : StringInputViewModel, IPrefillToken
    {
        public string PrefillToken { get; set; }
        private const string PrefillTokenParam = "Prefill Token";

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            PrefillToken = StringUtil.GetString(item.Fields[PrefillTokenParam]);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            item.Fields[PrefillTokenParam]?.SetValue(PrefillToken, true);
        }
    }
}