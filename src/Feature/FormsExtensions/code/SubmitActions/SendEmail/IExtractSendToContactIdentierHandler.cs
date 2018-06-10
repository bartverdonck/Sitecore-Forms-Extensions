using System.Collections.Generic;
using Sitecore.ExperienceForms.Processing;
using Sitecore.XConnect;

namespace Feature.FormsExtensions.SubmitActions.SendEmail
{
    public interface IExtractSendToContactIdentierHandler
    {
        IList<ContactIdentifier> GetContacts(SendEmailExtendedData data, FormSubmitContext formSubmitContext);
    }

}