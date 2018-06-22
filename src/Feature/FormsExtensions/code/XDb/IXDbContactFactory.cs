using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbContactFactory
    {
        IXDbContact CreateContact(string identifierValue);

        IXDbContactWithEmail CreateContactWithEmail(string email);
    }
}