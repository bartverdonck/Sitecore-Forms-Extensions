namespace Feature.FormsExtensions.Business.PrefillToken
{
    public interface IPrefillTokenHandler
    {
        ITokenHandlerResult GetTokenValue();
        void StoreTokenValue(object newValue);
    }
}