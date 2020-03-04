using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;

namespace Feature.FormsExtensions.ValueProviders.xDbFieldValueBinders.ContactPersonalInfo
{
    public class XDbNickNameFieldValueBinder : PersonalInformationFieldValueBinder {
        
        protected override IFieldValueBinderResult GetFieldBindingValueFromFacet(PersonalInformation facet)
        {
            if (string.IsNullOrEmpty(facet.Nickname))
                return new NoFieldValueBindingFoundResult();
            return new FieldValueBindingFoundResult(facet.Nickname);
        }

        public override void StoreValue(object newValue)
        {
            if (newValue is string nickName)
            {
               UpdateFacet(x => x.Nickname = nickName);
            }
            if (newValue is List<string> nickNameList)
            {
                UpdateFacet(x => x.Nickname = nickNameList.FirstOrDefault());
            }
        }
        
    }
}