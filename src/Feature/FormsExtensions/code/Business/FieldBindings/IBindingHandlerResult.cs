namespace Feature.FormsExtensions.Business.FieldBindings
{
    public interface IBindingHandlerResult
    {
        bool HasValue();
        object Value { get; }
    }
}