namespace Feature.FormsExtensions.XDb.Model
{
    public class FormsExtensionsXDbContact : IXDbContactWithEmail
    {
        public string IdentifierSource { get; protected set; } = "email";
        public string IdentifierValue => Email;
        public string Email { get; set; }

        public FormsExtensionsXDbContact()
        {
        }

        public FormsExtensionsXDbContact(string address)
        {
            Email = address;
        }

        public FormsExtensionsXDbContact(string address, string identifierSource)
        {
            Email = address;
            IdentifierSource = identifierSource;
        }
    }
}