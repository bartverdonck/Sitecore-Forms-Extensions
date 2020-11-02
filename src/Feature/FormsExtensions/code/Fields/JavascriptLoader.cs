using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Sitecore.ExperienceForms.Mvc.Constants;
using Sitecore.ExperienceForms.Mvc.Pipelines.RenderFields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields
{
    public class JavascriptLoader : MvcPipelineProcessor<RenderFieldsEventArgs>
    {
        public override void Process(RenderFieldsEventArgs args)
        {
            var scripts = HttpContext.Current.Items[IncludeFileKeys.ScriptsFilesKey] as Dictionary<string, IHtmlString>;

            var scriptKey = "formextensions-validate";
            if (scripts != null && !scripts.ContainsKey(scriptKey))
            {
                scripts.Add(scriptKey, new MvcHtmlString("<script src=\"/sitecore%20modules/Web/ExperienceForms/scripts/formsextensions.validate.js\"></script>"));
            }
        }
    }
}