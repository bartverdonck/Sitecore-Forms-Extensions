using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public class FormsExtensionsXDbContactFactory : IXDbContactFactory
    {
        public IXDbContact CreateContact(string identifierValue)
        {
            return CreateContactWithEmail(identifierValue);
        }

        public IXDbContactWithEmail CreateContactWithEmail(string email)
        {
            return new FormsExtensionsXDbContact(email);
        }
    }
}