using System.Web;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class ValueProviderContext
    {
        private const string ValueProviderConditionsMetKey = "ValueProviderConditionsMet";

        public static bool ValueProviderConditionsMet
        {
            get
            {
                if (HttpContext.Current.Items.Contains(ValueProviderConditionsMetKey))
                {
                    return (bool) HttpContext.Current.Items[ValueProviderConditionsMetKey];
                }
                return false;
            }
            set => HttpContext.Current.Items[ValueProviderConditionsMetKey] = value;
        }
    }
}