using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Sitecore.ExperienceForms.Data;
using Sitecore.ExperienceForms.Data.Entities;
using Sitecore.Services.Core.ComponentModel;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class AzureBlobStorageFileStorageProvider : IFileStorageProvider
    {
        private const string OriginalFileNameKey = "originalFileName";

        public string ConnectionString { get; set; }
        public string BlobContainer { get; set; }
        public string Folder { get; set; }
        
        public Guid StoreFile(Stream file, string fileName)
        {
            var id = Guid.NewGuid();
            var fileReference = GetFileReference(id);
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(BlobContainer);
            var blockBlob = blobContainer.GetBlockBlobReference(fileReference);
            blockBlob.UploadFromStream(file);
            blockBlob.Properties.AddProperty(OriginalFileNameKey, fileName);
            return id;
        }

        public Sitecore.ExperienceForms.Data.Entities.StoredFile GetFile(Guid fileId)
        {
            var blockBlob = GetCloudBlockBlob(fileId);
            var storedFile = new Sitecore.ExperienceForms.Data.Entities.StoredFile
            {
                FileInfo = new StoredFileInfo
                {
                    FileId = fileId,
                    FileName = blockBlob.Properties.ToDictionary()[OriginalFileNameKey] as string
                },
                File = GetFileStream(blockBlob)
            };
            return storedFile;
        }

        public void DeleteFiles(IEnumerable<Guid> fileIds)
        {
            throw new NotImplementedException();
        }

        public void CommitFiles(IEnumerable<Guid> fileIds)
        {
            throw new NotImplementedException();
        }

        public void Cleanup(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        private string GetFileReference(Guid id)
        {
            return Path.Combine(Folder, id.ToString());
        }

        private static Stream GetFileStream(CloudBlob blockBlob)
        {
            using (var fileStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(fileStream);
                return fileStream;
            }
        }

        private CloudBlockBlob GetCloudBlockBlob(Guid fileId)
        {
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(BlobContainer);
            var blockBlob = blobContainer.GetBlockBlobReference(Path.Combine(Folder, fileId.ToString()));
            blockBlob.FetchAttributes();
            return blockBlob;
        }
    }
}