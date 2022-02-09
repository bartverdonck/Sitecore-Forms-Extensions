using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public class FormsExtensionsXDbContactFactory : IXDbContactFactory
    {
        public IXDbContact CreateContact(string identifierValue, string identifierSource = null)
        {
            return CreateContactWithEmail(identifierValue, identifierSource);
        }

        public IXDbContactWithEmail CreateContactWithEmail(string email, string identifierSource = null)
        {
            return new FormsExtensionsXDbContact(email);
        }
    }
}