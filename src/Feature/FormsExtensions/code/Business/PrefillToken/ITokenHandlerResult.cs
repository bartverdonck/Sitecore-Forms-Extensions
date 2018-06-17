namespace Feature.FormsExtensions.Business.PrefillToken
{
    public interface ITokenHandlerResult
    {
        bool HasValue();
        object Value { get; }
    }
}