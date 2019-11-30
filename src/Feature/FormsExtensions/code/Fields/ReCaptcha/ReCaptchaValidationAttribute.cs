using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;

namespace Feature.FormsExtensions.Fields.ReCaptcha
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReCaptchaValidationAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly IReCaptchaService reCaptchaService = ServiceLocator.ServiceProvider.GetService<IReCaptchaService>();
        
        public override bool IsValid(object value)
        {
            var isValid = reCaptchaService.VerifySync((string) value);
            RecaptchaContext.RecaptchaValidated = isValid;
            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return Translate.Text(this.ErrorMessageString);
        }

        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "required"
            };
            yield return rule;
        }
        
    }
    
}