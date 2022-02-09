using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbContactFactory
    {
        IXDbContact CreateContact(string identifierValue, string identifierSource = null);

        IXDbContactWithEmail CreateContactWithEmail(string email, string identifierSource = null);
    }
}