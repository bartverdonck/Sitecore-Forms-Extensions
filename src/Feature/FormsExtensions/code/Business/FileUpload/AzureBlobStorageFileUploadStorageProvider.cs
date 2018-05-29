using System;
using System.IO;
using System.Web;
using Microsoft.WindowsAzure.Storage;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class AzureBlobStorageFileUploadStorageProvider : IFileUploadStorageProvider
    {
        public string ConnectionString { get; set; }
        public string BlobContainer { get; set; }

        public IStoredFile StoreFile(HttpPostedFileBase fileBase)
        {
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileBase.FileName)}";
            var file = StoreFile(fileBase, fileName);
            var storedFile = new StoredFile
            {
                Url = file.ToString(),
                OriginalFileName = fileBase.FileName,
                ContentType = fileBase.ContentType,
                ContentLength = fileBase.ContentLength
            };
            return storedFile;
        }

        private Uri StoreFile(HttpPostedFileBase fileBase, string fileName)
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(BlobContainer);
            var blockBlob = blobContainer.GetBlockBlobReference(fileName);
            blockBlob.UploadFromStream(fileBase.InputStream);
            return blockBlob.Uri;
        }
    }
}