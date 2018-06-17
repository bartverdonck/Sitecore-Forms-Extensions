using System;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    public class NoTokenValueFoundResult : ITokenHandlerResult
    {
        public bool HasValue()
        {
            return false;
        }

        public object Value => throw new Exception("There is no value available.");
    }
}