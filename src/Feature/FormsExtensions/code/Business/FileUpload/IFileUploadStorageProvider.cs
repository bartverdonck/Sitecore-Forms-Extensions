using System.Web;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public interface IFileUploadStorageProvider
    {
        IStoredFile StoreFile(HttpPostedFileBase fileBase);
    }
}