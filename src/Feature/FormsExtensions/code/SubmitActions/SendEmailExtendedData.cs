using System;
using Sitecore.EmailCampaign.Cd.Actions;

namespace Feature.FormsExtensions.SubmitActions
{
    public class SendEmailExtendedData : SendEmailData
    {
        public string Type { get; set; }
        public Guid FieldEmailAddressId { get; set; }
        public bool UpdateCurrentContact { get; set; }
        public string FixedEmailAddress { get; set; }
    }
}