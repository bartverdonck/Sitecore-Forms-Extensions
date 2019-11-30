using System;
using System.Collections.Generic;
using System.Linq;
using Feature.FormsExtensions.ApplicationSettings;
using Newtonsoft.Json.Linq;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.Modules.EmailCampaign.Messages;

namespace Feature.FormsExtensions.SubmitActions.SendEmail.FormsField
{
    public class FormDataProcessor
    {
        public void Process(SendMessageArgs args)
        {
            if (!(args.EcmMessage is MessageItem message))
                return;
            if (!message.CustomPersonTokens.ContainsKey(Constants.CustomTokensFormKey)) 
                return;
            var formFields = message.CustomPersonTokens[Constants.CustomTokensFormKey];
            if (formFields == null)
                return;
            message.CustomPersonTokens[Constants.CustomTokensFormKey] = ConvertToPlainText(formFields);
        }

        private static string ConvertToPlainText(object formFieldsObject)
        {
            var plainTextString = "";
            if (!(formFieldsObject is JArray json))
                return plainTextString;

            var formFields = json.ToObject<IList<FormField>>();
            foreach (var formField in formFields)
            {
                plainTextString += formField.Name + " : ";
                if (formField.Value != null)
                {
                    plainTextString += formField.Value.Name;
                }
                else
                {
                    plainTextString += formField.ValueList.Aggregate("", (current, value) => current + ", " + value.Name);
                }
                plainTextString += Environment.NewLine;
            }
            return plainTextString;
        }
    }
}