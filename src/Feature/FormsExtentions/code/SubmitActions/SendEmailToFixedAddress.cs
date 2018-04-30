using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtentions.Business;
using Feature.FormsExtentions.XDb;
using Feature.FormsExtentions.XDb.Model;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Cd.Services;
using Sitecore.EmailCampaign.Model.Messaging;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.XConnect;
using Constants = Feature.FormsExtentions.ApplicationSettings.Constants;

namespace Feature.FormsExtentions.SubmitActions
{
    public class SendEmailToFixedAddress : SubmitActionBase<SendEmailToFixedAddressData>
    {
        private readonly IClientApiService clientApiService;
        private readonly IXDbService xDbService;
        private readonly IFormFieldConverter formFieldConverter;
        private readonly ILogger logger;

        public SendEmailToFixedAddress(ISubmitActionData submitActionData) : this(submitActionData,ServiceLocator.ServiceProvider.GetService<IClientApiService>(), ServiceLocator.ServiceProvider.GetService<ILogger>(), ServiceLocator.ServiceProvider.GetService<IXDbService>(), ServiceLocator.ServiceProvider.GetService<IFormFieldConverter>())
        {
        }

        public SendEmailToFixedAddress(ISubmitActionData submitActionData, IClientApiService clientApiService, ILogger logger, IXDbService xDbService, IFormFieldConverter formFieldConverter)
          : base(submitActionData)
        {
            this.clientApiService = clientApiService;
            this.logger = logger;
            this.xDbService = xDbService;
            this.formFieldConverter = formFieldConverter;
        }

        protected internal virtual ContactIdentifier GetContactIdentifier(string address)
        {
            IServiceContact serviceContact = new ServiceContact(address);
            xDbService.CreateIfNotExists(serviceContact);
            return new ContactIdentifier(serviceContact.IdentifierSource, serviceContact.IdentifierValue, ContactIdentifierType.Known);
        }

        protected override bool Execute(SendEmailToFixedAddressData data, FormSubmitContext formSubmitContext)
        {
            if (data.MessageId == Guid.Empty)
            {
                logger.LogWarn("Empty message id");
                return false;
            }
            if (string.IsNullOrEmpty(data.To))
            {
                logger.LogError("To address is empty");
                return false;
            }
            try
            {
                var automatedMessage = new AutomatedMessage();
                automatedMessage.ContactIdentifier = GetContactIdentifier(data.To);
                automatedMessage.MessageId = data.MessageId;
                automatedMessage.CustomTokens = BuildCustomTokens(formSubmitContext);
                clientApiService.SendAutomatedMessage(automatedMessage);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return false;
            }
            return true;
        }

        private Dictionary<string, object> BuildCustomTokens(FormSubmitContext formSubmitContext)
        {
            var formFields = formFieldConverter.Convert(formSubmitContext.Fields);
            var customTokens = new Dictionary<string, object>();
            customTokens.Add(Constants.CustomTokensFormKey, formFields);
            foreach (var formField in formFields)
            {
                customTokens.Add($"form_{formField.Name}",GetSingleStringValue(formField));
            }
            return customTokens;
        }

        private static string GetSingleStringValue(FormField formField)
        {
            return formField.ValueList != null ? string.Join(", ", formField.ValueList.Select(x => x.Name)) : formField.Value.Name;
        }
    }
}