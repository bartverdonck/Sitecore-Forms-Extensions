using System;

namespace Feature.FormsExtensions.SubmitActions.IdentifyContact
{
    public class IdentifyContactData
    {
        public Guid? FieldIdentifyContactId { get; set; }

        public string IdentifierSource { get; set; } = "email";
    }
}