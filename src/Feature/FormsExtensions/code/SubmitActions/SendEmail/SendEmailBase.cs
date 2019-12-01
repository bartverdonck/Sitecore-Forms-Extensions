using System;
using System.Collections.Generic;
using Feature.FormsExtensions.SubmitActions.SendEmail.Tokens;
using Sitecore.EmailCampaign.Cd.Actions;
using Sitecore.EmailCampaign.Cd.Services;
using Sitecore.EmailCampaign.Model.Messaging;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public abstract class SendEmailBase<T> : SubmitActionBase<T> where T : SendEmailData
    {
        private readonly IClientApiService clientApiService;
        private readonly ILogger logger;
        private readonly IMailTokenBuilder mailTokenBuilder;
        
        protected SendEmailBase(ISubmitActionData submitActionData, ILogger logger, IClientApiService clientApiService, IMailTokenBuilder mailTokenBuilder) : base(submitActionData)
        {
            this.logger = logger;
            this.clientApiService = clientApiService;
            this.mailTokenBuilder = mailTokenBuilder;
        }

        protected override bool Execute(T data, FormSubmitContext formSubmitContext)
        {
            if (data.MessageId == Guid.Empty)
            {
                logger.LogWarn("Empty message id");
                return false;
            }
            var toContacts = GetToContacts(data, formSubmitContext);
            if (toContacts == null || toContacts.Count == 0)
            {
                return false;
            }
            try
            {
                var customTokens = BuildCustomTokens(data, formSubmitContext);
                foreach (var to in toContacts)
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

        protected virtual void SendMail(ContactIdentifier toContact, Dictionary<string, object> customTokens, Guid messageId)
        {
            var automatedMessage = new AutomatedMessage();
            automatedMessage.ContactIdentifier = toContact;
            automatedMessage.MessageId = messageId;
            automatedMessage.CustomTokens = customTokens;
            automatedMessage.TargetLanguage = Sitecore.Context.Language.Name;
            clientApiService.SendAutomatedMessage(automatedMessage);
        }

        protected virtual Dictionary<string, object> BuildCustomTokens(T data, FormSubmitContext formSubmitContext)
        {
            return mailTokenBuilder.BuildTokens(data.FieldsTokens, formSubmitContext);
        }
        
        protected abstract IList<ContactIdentifier> GetToContacts(T data, FormSubmitContext formSubmitContext);
        
    }
    
}