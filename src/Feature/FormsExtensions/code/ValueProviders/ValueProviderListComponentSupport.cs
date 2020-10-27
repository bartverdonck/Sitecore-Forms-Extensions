using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sitecore.Data.Items;

namespace Feature.FormsExtensions.ValueProviders
{
    public class ValueProviderListComponentSupport
    {
        private static List<Guid> _listComponentGuids;

        private static List<Guid> GetListComponentGuids()
        {
            return _listComponentGuids ?? (_listComponentGuids = Sitecore.Configuration.Settings
                .GetSetting("FormListComponentTemplates").Split('|').Select(id => new Guid(id)).ToList());
        }

        public static object MakeReturnListCompatible<T>(T value, Item fieldItem)
        {
            if (GetListComponentGuids().Contains(fieldItem.TemplateID.Guid))
            {
                return new List<T> { value };
            }

            return value;
        }
    }
}