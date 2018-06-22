using System;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class NoBindingValueFoundResult : IBindingHandlerResult
    {
        public bool HasValue()
        {
            return false;
        }

        public object Value => throw new Exception("There is no value available.");
    }
}