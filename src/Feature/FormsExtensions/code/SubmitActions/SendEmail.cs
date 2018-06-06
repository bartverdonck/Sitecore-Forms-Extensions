using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Business;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Model;
using Sitecore.EmailCampaign.Cd.Actions;
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
    public abstract class SendEmail<T> : SubmitActionBase<T> where T : SendEmailData
    {
        private readonly IClientApiService clientApiService;
        private readonly IXDbService xDbService;
        private readonly IFormFieldConverter formFieldConverter;
        private readonly ILogger logger;

        protected SendEmail(ISubmitActionData submitActionData, ILogger logger, IClientApiService clientApiService, IXDbService xDbService, IFormFieldConverter formFieldConverter) : base(submitActionData)
        {
            this.logger = logger;
            this.clientApiService = clientApiService;
            this.xDbService = xDbService;
            this.formFieldConverter = formFieldConverter;
        }

        protected override bool Execute(T data, FormSubmitContext formSubmitContext)
        {
            if (data.MessageId == Guid.Empty)
            {
                logger.LogWarn("Empty message id");
                return false;
            }
            var toAddresses = GetToAddresses(data, formSubmitContext);
            if (toAddresses == null || toAddresses.Count == 0)
            {
                return false;
            }
            try
            {
                var customTokens = BuildCustomTokens(formSubmitContext);
                foreach (var to in toAddresses)
                {
                    SendMail(to, customTokens, data.MessageId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return false;
            }
            return true;
        }

        protected virtual void SendMail(string to, Dictionary<string, object> customTokens, Guid messageId)
        {
            var automatedMessage = new AutomatedMessage();
            automatedMessage.ContactIdentifier = GetContactIdentifier(to.Trim());
            automatedMessage.MessageId = messageId;
            automatedMessage.CustomTokens = customTokens;
            clientApiService.SendAutomatedMessage(automatedMessage);
        }

        protected virtual ContactIdentifier GetContactIdentifier(string address)
        {
            IServiceContact serviceContact = new ServiceContact(address);
            xDbService.CreateIfNotExists(serviceContact);
            return new ContactIdentifier(serviceContact.IdentifierSource, serviceContact.IdentifierValue, ContactIdentifierType.Known);
        }

        private Dictionary<string, object> BuildCustomTokens(FormSubmitContext formSubmitContext)
        {
            var formFields = formFieldConverter.Convert(formSubmitContext.Fields);
            var customTokens = new Dictionary<string, object>();
            customTokens.Add(Constants.CustomTokensFormKey, formFields);
            foreach (var formField in formFields)
            {
                customTokens.Add($"form_{formField.Name}", GetSingleStringValue(formField));
            }
            return customTokens;
        }

        private static string GetSingleStringValue(FormField formField)
        {
            return formField.ValueList != null ? string.Join(", ", formField.ValueList.Select(x => x.Name)) : formField.Value.Name;
        }

        protected abstract IList<string> GetToAddresses(T data, FormSubmitContext formSubmitContext);
    }
    
}