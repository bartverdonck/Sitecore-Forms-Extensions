using System;
using System.Collections.Generic;
using Feature.FormsExtensions.SubmitActions.SendEmail.FileAttachment;
using Feature.FormsExtensions.SubmitActions.SendEmail.FormsField;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Cd.Services;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.Mvc.Extensions;
using Sitecore.XConnect;
    
namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public class SendEmailExtended : SendEmailBase<SendEmailExtendedData>
    {
        private readonly FixedAddressContactIdentierHandler fixedAddressHandler;
        private readonly FieldValueContactIdentierHandler fieldValueContactIdentierHandler;
        private readonly CurrentContactContactIdentierHandler currentContactContactIdentierHandler;
        private readonly FileAttachmentTokenBuilder fileAttachmentTokenBuilder;

        public SendEmailExtended(ISubmitActionData submitActionData) : this(submitActionData,
            ServiceLocator.ServiceProvider.GetService<ILogger>(),
            ServiceLocator.ServiceProvider.GetService<IClientApiService>(),
            ServiceLocator.ServiceProvider.GetService<IFormFieldConverter>(), 
            ServiceLocator.ServiceProvider.GetService<FixedAddressContactIdentierHandler>(), 
            ServiceLocator.ServiceProvider.GetService<FieldValueContactIdentierHandler>(),
            ServiceLocator.ServiceProvider.GetService<CurrentContactContactIdentierHandler>(),
            ServiceLocator.ServiceProvider.GetService<FileAttachmentTokenBuilder>())
        {
            
        }

        public SendEmailExtended(ISubmitActionData submitActionData, 
            ILogger logger, 
            IClientApiService clientApiService, 
            IFormFieldConverter formFieldConverter, 
            FixedAddressContactIdentierHandler fixedAddressHandler, 
            FieldValueContactIdentierHandler fieldValueContactIdentierHandler, 
            CurrentContactContactIdentierHandler currentContactContactIdentierHandler,
            FileAttachmentTokenBuilder fileAttachmentTokenBuilder) : base(submitActionData, logger, clientApiService, formFieldConverter)
        {
            this.fixedAddressHandler = fixedAddressHandler;
            this.fieldValueContactIdentierHandler = fieldValueContactIdentierHandler;
            this.currentContactContactIdentierHandler = currentContactContactIdentierHandler;
            this.fileAttachmentTokenBuilder = fileAttachmentTokenBuilder;
        }

        protected override IList<ContactIdentifier> GetToContacts(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            var handler = GetExtractSendToContactIdentierHandler(data.Type);
            return handler.GetContacts(data, formSubmitContext);
        }

        public IExtractSendToContactIdentierHandler GetExtractSendToContactIdentierHandler(string name)
        {
            switch (name)
            {
                case "fixedAddress": return fixedAddressHandler;
                case "currentContact": return currentContactContactIdentierHandler;
                case "fieldValue": return fieldValueContactIdentierHandler;
            }
            throw new Exception($"Unknown sendToType: {name}");
        }

        protected override Dictionary<string, object> BuildCustomTokens(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            var tokens = base.BuildCustomTokens(data, formSubmitContext);
            var attachmentTokens = fileAttachmentTokenBuilder.BuildFileAttachmentTokens(data,formSubmitContext);
            tokens.AddRange(attachmentTokens);
            return tokens;
        }
    }
}