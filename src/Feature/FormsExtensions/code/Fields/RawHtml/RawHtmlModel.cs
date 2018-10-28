using System;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.RawHtml
{
    [Serializable]
    public class RawHtmlModel : FieldViewModel
    {
        public string Html { get; set; }

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            Html = StringUtil.GetString(item.Fields["Html"]);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            item.Fields["Html"]?.SetValue(Html, true);
        }
    }
}