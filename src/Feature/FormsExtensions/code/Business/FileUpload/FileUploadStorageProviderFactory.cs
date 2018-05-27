using Sitecore.Configuration;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public interface IFileUploadStorageProviderFactory
    {
        IFileUploadStorageProvider GetDefaultFileUploadStorageProvider();
    }

    public class FileUploadStorageProviderFactory : IFileUploadStorageProviderFactory
    {
        public IFileUploadStorageProvider GetDefaultFileUploadStorageProvider()
        {
            var fileUploadConfigNode = Factory.GetConfigNode("formExtensions/fileUploadStorageProvider");
            return Factory.CreateObject<IFileUploadStorageProvider>(fileUploadConfigNode);
        }
    }
}