using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Fields.FileUpload;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
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