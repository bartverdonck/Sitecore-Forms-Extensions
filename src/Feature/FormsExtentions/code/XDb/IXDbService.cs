using Feature.FormsExtentions.XDb.Model;

namespace Feature.FormsExtentions.XDb
{
    public interface IXDbService
    {
        void IdentifyCurrent(IBasicContact contact);
        void CreateIfNotExists(IServiceContact contact);
    }
}
