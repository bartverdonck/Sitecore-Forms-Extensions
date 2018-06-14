using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb.Repository
{
    public interface IXDbContactRepository
    {
        void UpdateXDbContact(IXDbContact contact);

        void UpdateOrCreateXDbContact(IXDbContact contact);
    }
}
