using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Sitecore.ExperienceForms.Mvc.Models.Validation;

namespace Feature.FormsExtensions.Fields.FileUpload
{
    public class FileSizeValidation : ValidationElement<string>
    {
        protected int MaxFileSize { get; set; }

        public FileSizeValidation(ValidationDataModel validationItem) : base(validationItem)
        {

        }

        public override void Initialize(object validationModel)
        {
            base.Initialize(validationModel);
            if (!(validationModel is FileUploadModel fileUploadModel))
                return;
            MaxFileSize = fileUploadModel.MaxFileSize;
        }

        public override ValidationResult Validate(object value)
        {
            var postedFile = (HttpPostedFileBase) value;
            if (postedFile.ContentLength > MaxFileSize)
            {
                return new ValidationResult(FormatMessage(MaxFileSize));
            }
            return ValidationResult.Success;
        }
                

        public override IEnumerable<ModelClientValidationRule> ClientValidationRules
        {
            get
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = FormatMessage(MaxFileSize),
                    ValidationType = "filesize"
                };
                rule.ValidationParameters["maxfilesize"] = MaxFileSize;
                yield return rule;
            }
        }
    }
}