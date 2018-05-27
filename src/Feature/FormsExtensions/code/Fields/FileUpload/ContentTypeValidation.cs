using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Sitecore.ExperienceForms.Mvc.Models.Validation;

namespace Feature.FormsExtensions.Fields.FileUpload
{
    public class ContentTypeValidation : ValidationElement<string>
    {
        protected string AllowedContentTypes { get; set; }

        public ContentTypeValidation(ValidationDataModel validationItem) : base(validationItem)
        {

        }

        public override void Initialize(object validationModel)
        {
            base.Initialize(validationModel);
            if (!(validationModel is FileUploadModel fileUploadModel))
                return;
            AllowedContentTypes = fileUploadModel.AllowedContentTypes;
        }

        public override ValidationResult Validate(object value)
        {
            var postedFile = (HttpPostedFileBase) value;
            if (AllowedContentTypes.Contains(postedFile.ContentType))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(FormatMessage(postedFile.ContentType));
        }
                

        public override IEnumerable<ModelClientValidationRule> ClientValidationRules
        {
            get
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = FormatMessage(),
                    ValidationType = "contenttype"
                };
                rule.ValidationParameters["allowedcontenttypes"] = AllowedContentTypes;
                yield return rule;
            }
        }
    }
}