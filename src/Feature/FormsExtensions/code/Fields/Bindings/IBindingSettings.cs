namespace Feature.FormsExtensions.Fields.Bindings
{
    public interface IBindingSettings
    {
        string BindingToken { get; set; }
        bool PrefillBindingValue { get; set; }
        bool StoreBindingValue { get; set; }
    }
}