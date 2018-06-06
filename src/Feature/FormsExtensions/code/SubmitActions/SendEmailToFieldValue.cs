using System;
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
    public class SendEmailToFieldValue : SendEmail<SendEmailExtendedData>
    {
        protected override IList<string> GetToAddresses(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            var field = GetFieldById(data.FieldEmailAddressId, formSubmitContext.Fields);
            var toAddresses = GetValue(field);
            return toAddresses.Split(';').ToList();
        }

        private static IViewModel GetFieldById(Guid id, IList<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
        }

        private static string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }

        public SendEmailToFieldValue(ISubmitActionData submitActionData, ILogger logger, IClientApiService clientApiService, IXDbService xDbService, IFormFieldConverter formFieldConverter) : base(submitActionData, logger, clientApiService, xDbService, formFieldConverter)
        {
        }
    }
}