namespace Feature.FormsExtensions.Business.PrefillToken
{
    public class TokenValueFoundResult : ITokenHandlerResult
    {
        public TokenValueFoundResult(object value)
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