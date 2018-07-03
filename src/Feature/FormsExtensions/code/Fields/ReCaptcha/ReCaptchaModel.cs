using System;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;

namespace Feature.FormsExtensions.Fields.ReCaptcha
{
    [Serializable]
    public class ReCaptchaModel : FieldViewModel, IValueField
    {
        [ReCaptchaValidation(ErrorMessage = "captcha.required")]
        public string CaptchaValue { get; set; }

        public string CaptchaPublicKey => Sitecore.Configuration.Settings.GetSetting("GoogleCaptchaPublicKey");

        public bool Required { get; set; }
        public bool IsTrackingEnabled { get; set; }
        public bool AllowSave { get; set; }
    }
}