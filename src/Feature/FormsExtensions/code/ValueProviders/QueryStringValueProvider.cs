using System.Web;
using Sitecore.ExperienceForms.ValueProviders;

namespace Feature.FormsExtensions.ValueProviders
{
    public class QueryStringValueProvider : IFieldValueProvider
    {
        public object GetValue(string parameter)
        {
            var queryParam = parameter;
            if (string.IsNullOrEmpty(queryParam))
            {
                queryParam = ValueProviderContext.FieldItem.Name;
            }
            var value = HttpContext.Current.Request.QueryString[queryParam];
            return ValueProviderListComponentSupport.MakeReturnListCompatible(value, ValueProviderContext.FieldItem);
        }

        public FieldValueProviderContext ValueProviderContext { get; set; }
    }
}