using System;
using System.ComponentModel.DataAnnotations;
using Sitecore.ExperienceForms.RobotDetection;
using Sitecore.Globalization;

namespace Feature.FormsExtensions.Fields.RobotDetection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RobotDetectionValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return RobotDetectionHelper.IsContactClassificationGuessed;
        }

        public override string FormatErrorMessage(string name)
        {
            return Translate.Text(this.ErrorMessageString);
        }
    }
}