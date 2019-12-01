using System.Collections.Generic;
using Sitecore.ExperienceForms.Processing;

namespace Feature.FormsExtensions.SubmitActions.SendEmail.Tokens
{
    public interface IMailTokenBuilder
    {
        Dictionary<string, object> BuildTokens(IDictionary<string, string> fieldTokens, FormSubmitContext formSubmitContext);
        Dictionary<string, object> BuildAllTokens(FormSubmitContext formSubmitContext);
    }
}