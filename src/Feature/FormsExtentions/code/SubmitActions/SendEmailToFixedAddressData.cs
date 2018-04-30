using Sitecore.EmailCampaign.Cd.Actions;

namespace Feature.FormsExtentions.SubmitActions
{
    public class SendEmailToFixedAddressData : SendEmailData
    {
        public string To { get; set; }   
    }
}