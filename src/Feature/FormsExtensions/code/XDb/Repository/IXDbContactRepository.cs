using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb.Repository
{
    public interface IXDbContactRepository
    {
        void UpdateXDbContact(IBasicContact contact);

        void UpdateServiceContact(IServiceContact serviceContact);
    }
}
