using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Sitecore.ExperienceForms.Data;
using Sitecore.ExperienceForms.Data.Entities;

namespace Feature.FormsExtensions.FileStorageProviders
{
    public class AzureBlobStorageFileStorageProvider : IFileStorageProvider
    {
        private const string OriginalFileNameKey = "originalFileName";
        private const string CommitedKey = "commited";

        public string ConnectionString => Sitecore.Configuration.Settings.GetSetting("AzureBlobStorageFileStorageProvider.ConnectionString");
        public string BlobContainer => Sitecore.Configuration.Settings.GetSetting("AzureBlobStorageFileStorageProvider.BlobContainer");
        public string Folder => Sitecore.Configuration.Settings.GetSetting("AzureBlobStorageFileStorageProvider.Folder");

        public Guid StoreFile(Stream file, string fileName)
        {
            var id = Guid.NewGuid();
            var fileReference = GetFileReference(id);
            var storageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(BlobContainer);
            var blockBlob = blobContainer.GetBlockBlobReference(fileReference);
            blockBlob.UploadFromStream(file);
            blockBlob.Metadata.Add(OriginalFileNameKey,fileName);
            blockBlob.Metadata.Add(CommitedKey, bool.FalseString);
            blockBlob.SetMetadata();
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
                    FileName = blockBlob.Metadata[OriginalFileNameKey]
                },
                File = GetFileStream(blockBlob)
            };
            return storedFile;
        }

        public void DeleteFiles(IEnumerable<Guid> fileIds)
        {
            foreach (var fileId in fileIds)
            {
                var blockBlob = GetCloudBlockBlob(fileId);
                blockBlob.DeleteIfExists(DeleteSnapshotsOption.IncludeSnapshots);
            }
        }

        public void CommitFiles(IEnumerable<Guid> fileIds)
        {
            foreach (var fileId in fileIds)
            {
                var blockBlob = GetCloudBlockBlob(fileId);
                blockBlob.Metadata[CommitedKey] = bool.TrueString;
                blockBlob.SetMetadata();
            }
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
            var fileStream = new MemoryStream();
            blockBlob.DownloadToStream(fileStream);
            return fileStream;
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