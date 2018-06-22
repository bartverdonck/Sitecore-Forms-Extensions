using System.Collections.Generic;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public interface IFieldBindingMapFactory
    {
        Dictionary<FieldBindingTokenKey, IBindingHandler> GetFieldBindingTokenMap();
    }
}