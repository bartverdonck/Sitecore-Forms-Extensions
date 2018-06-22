namespace Feature.FormsExtensions.Business.FieldBindings
{
    public interface IBindingHandler
    {
        IBindingHandlerResult GetBindingValue();
        void StoreBindingValue(object newValue);
    }
}