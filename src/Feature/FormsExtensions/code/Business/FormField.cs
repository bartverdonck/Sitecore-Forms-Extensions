using System;
using System.Collections.Generic;

namespace Feature.FormsExtensions.Business
{
    public class FormField
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FormFieldValue Value { get; set; }
        public IList<FormFieldValue> ValueList { get; set; }
        
    }

    public class FormFieldValue
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}