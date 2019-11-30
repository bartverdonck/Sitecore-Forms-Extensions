namespace Feature.FormsExtensions.ValueProviders
{
    public class FieldValueBindingFoundResult : IFieldValueBinderResult
    {
        public FieldValueBindingFoundResult(object value)
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