using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Sitecore.ExperienceForms.Mvc.Constants;

namespace Feature.FormsExtensions.Business.Html
{
    public static class ScriptStore
    {
        public static string CaptchaScriptKey => "google-recaptcha";
        public static string ValidateScriptKey => "formextensions-validate";

        public static void AddScript(this HtmlHelper htmlHelper, string key, IHtmlString value)
        {
            var scripts = htmlHelper.ViewContext.HttpContext.Items[IncludeFileKeys.ScriptsFilesKey] as Dictionary<string, IHtmlString>;
            if(scripts!=null && !scripts.ContainsKey(key)) { 
                scripts.Add(key, value);
            }
        }
    }
}