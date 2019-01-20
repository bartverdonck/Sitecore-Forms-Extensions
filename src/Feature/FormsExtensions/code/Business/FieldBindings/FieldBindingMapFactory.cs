using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactAddress;
using Feature.FormsExtensions.Business.FieldBindings.xDbBindingHandlers.ContactPersonalInfo;
using Feature.FormsExtensions.XDb;
using Feature.FormsExtensions.XDb.Repository;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.Models;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class FieldBindingMapFactory : IFieldBindingMapFactory
    {
        public IBindingHandler GetBindingHandler(ValueProviderSettings valueProviderSettings)
        {
            string str = valueProviderSettings?.ValueProviderItemId;
            if (!ID.IsID(str)) 
                return null;
            Item obj1 = Sitecore.Context.Database.GetItem(str);
            string modelType = obj1?["Model Type"];
            if (string.IsNullOrEmpty(modelType))
                return null;
            return new XDbFirstNameBindingHandler();
        }
    }
}