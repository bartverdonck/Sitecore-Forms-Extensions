using System;
using System.Collections.Generic;
using Feature.FormsExtensions.Business;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.EmailCampaign.Cd.Services;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
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

      
    }
}