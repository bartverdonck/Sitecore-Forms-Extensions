using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.Fields.ValueResolvers;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;

namespace Feature.FormsExtensions.SubmitActions.SendEmail.Tokens
{
    public class MailTokenBuilder : IMailTokenBuilder
    {
        private readonly IFormsFieldValueResolver formsFieldValueResolver;

        public MailTokenBuilder(IFormsFieldValueResolver formsFieldValueResolver)
        {
            this.formsFieldValueResolver = formsFieldValueResolver;
        }

        public Dictionary<string, object> BuildTokens(IDictionary<string, string> fieldTokens, FormSubmitContext formSubmitContext)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var fieldToken in fieldTokens)
            {
                dictionary.Add(fieldToken.Key, GetFieldValue(formSubmitContext, fieldToken.Value));    
            }

            return dictionary;
        }

        public Dictionary<string, object> BuildAllTokens(FormSubmitContext formSubmitContext)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var viewModel in formSubmitContext.Fields)
            {
                var stringValue = formsFieldValueResolver.GetStringFieldValue(viewModel);
                if (stringValue != null) { 
                    dictionary.Add(viewModel.Name, stringValue);
                }
            }

            return dictionary;
        }

        private string GetFieldValue(FormSubmitContext formSubmitContext, string fieldId)
        {
            var field = GetFieldByIdFromSubmitContext(formSubmitContext, fieldId);

            if (field == null)
            {
                return string.Empty;
            }

            return formsFieldValueResolver.GetStringFieldValue(field)??string.Empty;
        }

        private static IViewModel GetFieldByIdFromSubmitContext(FormSubmitContext formSubmitContext, string fieldId)
        {
            return formSubmitContext.Fields.SingleOrDefault(x => x.ItemId.Equals(fieldId, StringComparison.OrdinalIgnoreCase));
        }
    }
}