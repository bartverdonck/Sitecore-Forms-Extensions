using System.Collections.Concurrent;
using Sitecore;
using Sitecore.Data;
using Sitecore.ExperienceForms.Mvc.Models;
using Sitecore.Reflection;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class FieldBindingMapFactory : IFieldBindingMapFactory
    {
        private static readonly ConcurrentDictionary<string, IBindingHandler> BindingHandlers = new ConcurrentDictionary<string, IBindingHandler>();

        public IBindingHandler GetBindingHandler(ValueProviderSettings valueProviderSettings)
        {
            var str = valueProviderSettings?.ValueProviderItemId;
            if (!ID.IsID(str)) 
                return null;
            var valueProviderItem = Context.Database.GetItem(str);
            var modelType = valueProviderItem?["Model Type"];
            return string.IsNullOrEmpty(modelType) ? null : CreateBindingHandler(modelType);
        }
        
        public IBindingHandler CreateBindingHandler(string modelType)
        {
            if (BindingHandlers.TryGetValue(modelType, out var bindingHandler))
            {
                return bindingHandler;
            }
            var typeInfo = ReflectionUtil.GetTypeInfo(modelType);
            if (typeInfo == null)
                return null;
            bindingHandler = ReflectionUtil.CreateObject(typeInfo) as IBindingHandler;
            if (bindingHandler != null)
                BindingHandlers.TryAdd(modelType, bindingHandler);
            return bindingHandler;
        }
    }
}