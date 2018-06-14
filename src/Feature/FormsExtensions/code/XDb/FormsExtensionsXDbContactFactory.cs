using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public class FormsExtensionsXDbContactFactory : IXDbContactFactory
    {
        public IXDbContact CreateContact(string email)
        {
            return new FormsExtensionsXDbContact(email);
        }
    }
}