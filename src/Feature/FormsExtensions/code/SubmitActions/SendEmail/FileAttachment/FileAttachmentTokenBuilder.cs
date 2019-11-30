using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.ExM.Framework.Diagnostics;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.ExperienceForms.Processing;

namespace Feature.FormsExtensions.SubmitActions.SendEmail.FileAttachment
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
            if (data.FileUploadFieldsToAttach == null)
                return tokens;
            foreach (var attachmentFieldId in data.FileUploadFieldsToAttach)
            {
                var field = GetFieldById(attachmentFieldId, formSubmitContext.Fields);
                if (field is null)
                {
                    logger.LogWarn($"Could not find field with id {data.FieldEmailAddressId}");
                    continue;
                }
                if (field.Value != null && field.Value.Count>0)
                {
                    for (var i = 0; i < field.Value.Count; i++)
                    {
                        tokens.Add($"attachment_{attachmentFieldId}_{i}", field.Value[i]);
                    }
                }
            }
            return tokens;
        }

        private static FileUploadViewModel GetFieldById(Guid id, IEnumerable<IViewModel> fields)
        {
            return fields.FirstOrDefault(f => Guid.Parse(f.ItemId) == id) as FileUploadViewModel;
        }

    }
}