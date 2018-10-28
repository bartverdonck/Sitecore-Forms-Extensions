using Sitecore.Mvc.Helpers;

namespace Feature.FormsExtensions.Views
{
    public static class HtmlHelper
    {
        public static bool IsExperienceFormsEditMode(this SitecoreHelper helper)
        {
            return Sitecore.Context.Request.QueryString["sc_formmode"] != null;
        }
    }
}