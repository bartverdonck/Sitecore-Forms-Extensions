using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Business;
using Feature.FormsExtensions.XDb;
using Sitecore.EmailCampaign.Cd.Services;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;

namespace Feature.FormsExtensions.SubmitActions
{
    public class SendEmailToFixedAddress : SendEmail<SendEmailToFixedAddressData>
    {
        private readonly ILogger logger;

        protected override IList<string> GetToAddresses(SendEmailToFixedAddressData data, FormSubmitContext formSubmitContext)
        {
            if (string.IsNullOrEmpty(data.To))
            {
                logger.LogError("To address is empty");
                return new List<string>();
            }
            return data.To.Split(';').ToList();
        }

        public SendEmailToFixedAddress(ISubmitActionData submitActionData, ILogger logger, IClientApiService clientApiService, IXDbService xDbService, IFormFieldConverter formFieldConverter) : base(submitActionData, logger, clientApiService, xDbService, formFieldConverter)
        {
            this.logger = logger;
        }
    }
}