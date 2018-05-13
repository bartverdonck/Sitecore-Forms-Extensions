using System.Web;
using Feature.FormsExtensions.Business.FileUpload;
using Sitecore.ExperienceForms.Mvc.Models.Validation;

namespace Feature.FormsExtensions.Fields.FileUpload
{
    public class FileUploadModel : ValueNotValidatedInputViewModel<IStoredFile>
    {
        [DynamicRequired]
        [DynamicValidation]
        public virtual HttpPostedFileBase File { get; set; }

    }
}