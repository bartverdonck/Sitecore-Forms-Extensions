namespace Feature.FormsExtensions.Fields.Prefill
{
    internal interface IBindingSettings
    {
        string BindingToken { get; set; }
        bool PrefillBindingValue { get; set; }
        bool StoreBindingValue { get; set; }
    }
}
