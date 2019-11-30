using System;

namespace Feature.FormsExtensions.ValueProviders
{
    public class NoFieldValueBindingFoundResult : IFieldValueBinderResult
    {
        public bool HasValue()
        {
            return false;
        }

        public object Value => throw new Exception("There is no value available.");
    }
}