using Sitecore.EmailCampaign.Cd.Actions;

namespace Feature.FormsExtensions.SubmitActions
{
    public class SendEmailToFixedAddressData : SendEmailData
    {
        public string To { get; set; }   
    }
}