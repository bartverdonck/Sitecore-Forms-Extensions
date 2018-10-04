using System;
using System.IO;
using System.Web;
using Feature.FormsExtensions.Fields.FileUpload;
using Microsoft.WindowsAzure.Storage;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class AzureBlobStorageFileUploadStorageProvider : IFileUploadStorageProvider
    {
        public string ConnectionString { get; set; }
        public string BlobContainer { get; set; }
        public string Folder { get; set; }

        public IStoredFile StoreFile(FileUploadModel fileUploadModel, Guid formId)
        {
            var fileBase = fileUploadModel.File;
            var filePath = FolderBuilder.BuildFolder(Folder,fileUploadModel, formId);
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileBase.FileName)}";
            var file = UploadFile(fileBase, Path.Combine(filePath,fileName));
            var storedFile = new StoredFile
            {
                Url = file.ToString(),
                OriginalFileName = fileBase.FileName,
                ContentType = fileBase.ContentType,
                ContentLength = fileBase.ContentLength,
                StoredFileName = fileName,
                StoredFilePath = filePath
            };
            return storedFile;
        }
        
        private Uri UploadFile(HttpPostedFileBase fileBase, string fileName)
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(BlobContainer);
            var blockBlob = blobContainer.GetBlockBlobReference(fileName);
            blockBlob.UploadFromStream(fileBase.InputStream);
            return blockBlob.Uri;
        }

        public byte[] GetFileAsBytes(IStoredFile storedFile)
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(BlobContainer);
            var blockBlob = blobContainer.GetBlockBlobReference(Path.Combine(storedFile.StoredFilePath,storedFile.StoredFileName));
            blockBlob.FetchAttributes();
            var fileByteLength = blockBlob.Properties.Length;
            var fileContent = new byte[fileByteLength];
            for (var i = 0; i < fileByteLength; i++)
            {
                fileContent[i] = 0x20;
            }
            blockBlob.DownloadToByteArray(fileContent, 0);
            return fileContent;
        }
    }
}