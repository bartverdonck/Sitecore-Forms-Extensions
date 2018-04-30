using Feature.FormsExtentions.XDb.Model;

namespace Feature.FormsExtentions.XDb.Repository
{
    public interface IXDbContactRepository
    {
        void UpdateXDbContact(IBasicContact contact);

        void UpdateServiceContact(IServiceContact serviceContact);
    }
}
