namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class BindingValueFoundResult : IBindingHandlerResult
    {
        public BindingValueFoundResult(object value)
        {
            Value = value;
        }

        public bool HasValue()
        {
            return true;
        }

        public object Value { get; }
    }
}