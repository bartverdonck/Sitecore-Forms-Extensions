using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.XDb;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;

namespace Feature.FormsExtensions.SubmitActions.IdentifyContact
{
    public class IdentifyContactAction : SubmitActionBase<IdentifyContactData>
    {
        private readonly IXDbService xDbService;
        private readonly IXDbContactFactory xDbContactFactory;
        private readonly ILogger logger;

        public IdentifyContactAction(ISubmitActionData submitActionData) : this(submitActionData,
            ServiceLocator.ServiceProvider.GetService<ILogger>(),
            ServiceLocator.ServiceProvider.GetService<IXDbService>(),
            ServiceLocator.ServiceProvider.GetService<IXDbContactFactory>())
        {
        }

        public IdentifyContactAction(ISubmitActionData submitActionData, ILogger logger, IXDbService xDbService, IXDbContactFactory xDbContactFactory) : base(submitActionData)
        {
            this.logger = logger;
            this.xDbService = xDbService;
            this.xDbContactFactory = xDbContactFactory;
        }

        protected override bool Execute(IdentifyContactData data, FormSubmitContext formSubmitContext)
        {
            if (data.FieldIdentifyContactId == null || data.FieldIdentifyContactId == Guid.Empty)
            {
                logger.LogWarn("Empty fieldIdentifyContact id");
                return false;
            }

            var field = GetFieldById(data.FieldIdentifyContactId.Value, formSubmitContext.Fields);
            var contact = xDbContactFactory.CreateContact(GetValue(field));
            if (string.IsNullOrEmpty(contact.IdentifierValue))
                return true;
            xDbService.IdentifyCurrent(contact);
            return true;
        }

        private static IViewModel GetFieldById(Guid id, IEnumerable<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id);
        }

        private static string GetValue(object field)
        {
            return field?.GetType().GetProperty("Value")?.GetValue(field, null)?.ToString() ?? string.Empty;
        }
    }
}