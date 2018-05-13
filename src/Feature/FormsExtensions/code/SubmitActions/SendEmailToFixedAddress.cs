using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Business;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Model;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Cd.Services;
using Sitecore.EmailCampaign.Model.Messaging;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.XConnect;
using Constants = Feature.FormsExtensions.ApplicationSettings.Constants;

namespace Feature.FormsExtensions.SubmitActions
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
                var customTokens = BuildCustomTokens(formSubmitContext);
                foreach (var to in data.To.Split(';'))
                {
                    SendEmail(to, customTokens, data.MessageId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return false;
            }
            return true;
        }

        private void SendEmail(string to, Dictionary<string, object> customTokens, Guid messageId)
        {
            var automatedMessage = new AutomatedMessage();
            automatedMessage.ContactIdentifier = GetContactIdentifier(to.Trim());
            automatedMessage.MessageId = messageId;
            automatedMessage.CustomTokens = customTokens;
            clientApiService.SendAutomatedMessage(automatedMessage);
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