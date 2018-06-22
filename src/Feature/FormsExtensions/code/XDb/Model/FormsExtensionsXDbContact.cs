namespace Feature.FormsExtensions.XDb.Model
{
    public class FormsExtensionsXDbContact : IXDbContactWithEmail
    {
        public string IdentifierSource => "email";
        public string IdentifierValue => Email;
        public string Email { get; set; }

        public FormsExtensionsXDbContact()
        {
        }

        public FormsExtensionsXDbContact(string address)
        {
            Email = address;
        }
    }
}