using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbService
    {
        void IdentifyCurrent(IXDbContact contact);
        void UpdateOrCreate(IXDbContact contact);
    }
}
