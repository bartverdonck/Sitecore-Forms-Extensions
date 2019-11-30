using System;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.ValueProviders;

namespace Feature.FormsExtensions.Fields.RobotDetection
{
    [Serializable]
    public class RobotDetectionModel : FieldViewModel, IValueField
    {
        [RobotDetectionValidation(ErrorMessage = "robotdetection.robotdetected")]
        public string DummyValue { get; set; }

        public void InitializeValue(FieldValueProviderContext context)
        {
            //Irrelevant for robotdetection.
        }

        public bool Required { get; set; }
        public bool IsTrackingEnabled { get; set; }
        public bool AllowSave { get; set; }
    }
}