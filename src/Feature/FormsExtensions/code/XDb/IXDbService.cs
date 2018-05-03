using Feature.FormsExtensions.XDb.Model;

namespace Feature.FormsExtensions.XDb
{
    public interface IXDbService
    {
        void IdentifyCurrent(IBasicContact contact);
        void CreateIfNotExists(IServiceContact contact);
    }
}
