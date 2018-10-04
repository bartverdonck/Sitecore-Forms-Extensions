using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Business;
using Feature.FormsExtensions.Fields.FileUpload;
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

        public SendEmailExtended(ISubmitActionData submitActionData) : this(submitActionData,
            ServiceLocator.ServiceProvider.GetService<ILogger>(),
            ServiceLocator.ServiceProvider.GetService<IClientApiService>(),
            ServiceLocator.ServiceProvider.GetService<IFormFieldConverter>(), 
            ServiceLocator.ServiceProvider.GetService<FixedAddressContactIdentierHandler>(), 
            ServiceLocator.ServiceProvider.GetService<FieldValueContactIdentierHandler>(),
            ServiceLocator.ServiceProvider.GetService<CurrentContactContactIdentierHandler>())
        {
            
        }

        public SendEmailExtended(ISubmitActionData submitActionData, 
            ILogger logger, 
            IClientApiService clientApiService, 
            IFormFieldConverter formFieldConverter, 
            FixedAddressContactIdentierHandler fixedAddressHandler, 
            FieldValueContactIdentierHandler fieldValueContactIdentierHandler, 
            CurrentContactContactIdentierHandler currentContactContactIdentierHandler) : base(submitActionData, logger, clientApiService, formFieldConverter)
        {
            this.fixedAddressHandler = fixedAddressHandler;
            this.fieldValueContactIdentierHandler = fieldValueContactIdentierHandler;
            this.currentContactContactIdentierHandler = currentContactContactIdentierHandler;
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
            var tokenBuilder = new FileAttachmentTokenBuilder(null);
            var attachmentTokens = tokenBuilder.BuildFileAttachmentTokens(data,formSubmitContext);
            tokens.AddRange(attachmentTokens);
            return tokens;
        }
    }

    public class FileAttachmentTokenBuilder
    {
        private readonly ILogger logger;

        public FileAttachmentTokenBuilder(ILogger logger)
        {
            this.logger = logger;
        }

        public Dictionary<string, object> BuildFileAttachmentTokens(SendEmailExtendedData data, FormSubmitContext formSubmitContext)
        {
            var tokens = new Dictionary<string, object>();
            foreach (var attachmentFieldId in data.FileUploadFieldsToAttach)
            {
                var field = GetFieldById(attachmentFieldId, formSubmitContext.Fields);
                if (field is null)
                {
                    logger.LogWarn($"Could not find field with id {data.FieldEmailAddressId}");
                    continue;
                }
                if (field.Value != null)
                {
                    tokens.Add($"attachment_{attachmentFieldId}",field.Value);
                   // tokens.Add($"attachment_fileName_{attachmentFieldId}", field.Value.OriginalFileName);
                    //tokens.Add($"attachment_url_{attachmentFieldId}", field.Value.Url);
                }
            }
            return tokens;
        }

        private static FileUploadModel GetFieldById(Guid id, IEnumerable<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id) as FileUploadModel;
        }

    }
}