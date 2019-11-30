using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sitecore.ExperienceForms.Models;

namespace Feature.FormsExtensions.SubmitActions.SendEmail.FormsField
{
    public class FormFieldConverter : IFormFieldConverter
    {

        public IList<FormField> Convert(IList<IViewModel> postedFields)
        {
            return (from postedField in postedFields where IsValidField(postedField) select Convert(postedField)).ToList();
        }

        private static bool IsValidField(IViewModel postedField)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var valueField = postedField as IValueField;
            var property = valueField?.GetType().GetProperty("Value");
            return (object) property != null;
        }

        private static FormField Convert(IViewModel postedField)
        {
            var formField = new FormField();
            formField.Name = postedField.Name;
            formField.Id = new Guid(postedField.ItemId);
            AddValues(formField, postedField);
            return formField;
        }

        private static void AddValues(FormField formField, IViewModel postedField)
        {
            var valueObject = GetPostedValueObject(postedField);
            if (valueObject is IList list)
            {
                formField.ValueList = new List<FormFieldValue>();
                foreach (var valueItemObject in list)
                {
                    formField.ValueList.Add(CreateFormFieldValue(valueItemObject));
                }
            }
            else
            {
                formField.Value = CreateFormFieldValue(valueObject);
            }
        }

        private static FormFieldValue CreateFormFieldValue(object valueObject)
        {
            var ffv = new FormFieldValue();
            if (valueObject == null)
                return ffv;
            ffv.Name = valueObject.ToString();
            if (Guid.TryParse(valueObject.ToString(), out var guidOutput))
            {
                ffv.Id = guidOutput;
            }
            return ffv;
        }

        private static object GetPostedValueObject(IViewModel postedField)
        {
            var property = postedField.GetType().GetProperty("Value");
            return (object) property != null?property.GetValue(postedField):null;
        }
    }
}