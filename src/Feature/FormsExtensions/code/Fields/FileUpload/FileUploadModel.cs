using System.Globalization;
using System.Web;
using Feature.FormsExtensions.Business.FileUpload;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models.Validation;

namespace Feature.FormsExtensions.Fields.FileUpload
{
    public class FileUploadModel : ValueNotValidatedInputViewModel<IStoredFile>
    {
        private const string AllowedContentTypesParam = "Allowed Content Types";
        private const string MaxFileSizeParam = "Max File Size";

        [DynamicRequired]
        [DynamicValidation]
        public virtual HttpPostedFileBase File { get; set; }

        public string AllowedContentTypes { get; set; }
        public int MaxFileSize { get; set; }

        protected override void InitItemProperties(Item item)
        {
            base.InitItemProperties(item);
            AllowedContentTypes = StringUtil.GetString(item.Fields[AllowedContentTypesParam]);
            MaxFileSize = MainUtil.GetInt(item.Fields[MaxFileSizeParam]?.Value, 0);
        }

        protected override void UpdateItemFields(Item item)
        {
            base.UpdateItemFields(item);
            item.Fields[AllowedContentTypesParam]?.SetValue(AllowedContentTypes, true);
            item.Fields[MaxFileSizeParam]?.SetValue(MaxFileSize.ToString(CultureInfo.InvariantCulture),true);
        }
    }
}