using Feature.FormsExtensions.Fields.FileUpload;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaLibraryFileUploadStorageProvider : IFileUploadStorageProvider
    {
        public string EventQueueDatabase { get; set; }
        public string DefaultRootStoragePath { get; set; }
        public bool Versioned { get; set; }
        public bool Overwrite { get; set; }

        public UploadDestination UploadDestination { get; set; } = UploadDestination.Database;

        public byte[] GetFileAsBytes(IStoredFile storedFile)
        {
            var mediaItem = (MediaItem)Context.Database.GetItem(storedFile.Url);
            if (mediaItem.InnerItem["file path"].Length > 0)
                return new byte[] { };

            var blobField = mediaItem.InnerItem.Fields["blob"];

            var stream = blobField.GetBlobStream();
            if (stream == null) return new byte[] { };

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            var memoryStream = new MemoryStream(bytes);
            return memoryStream.ToArray();
        }

        public IStoredFile StoreFile(FileUploadModel fileUploadModel, Guid formId)
        {
            try
            {
                var file = fileUploadModel.File;
                var folder = BuildFolder(fileUploadModel, formId);
                return Upload(file, folder);
            }
            catch (Exception ex)
            {
                Log.Error("[MediaLibraryFileUploadStorageProvider]: An error occurred while attempting to upload a file to media library.", ex, this);
                return StoredFile.EmptyStoredFile;
            }
        }

        protected string BuildFolder(FileUploadModel fileUploadModel, Guid formId)
        {
            var path = this.DefaultRootStoragePath;

            var folder = MediaLibraryFolderBuilder.BuildFolder(new BuildMediaLibraryFolderParameter
            {
                Path = path,
                FormId = formId.ToString()
            });
            return folder;
        }

        protected StoredFile Upload(HttpPostedFileBase file, string folder)
        {
            var fileBinary = ReadFileBinary(file);

            var mediaUploadResults = new MediaUploaderEx
            {
                File = new PostedFile(fileBinary, file.FileName, folder),
                Unpack = false,
                Folder = folder,
                Versioned = Versioned,
                Language = Context.Language,
                AlternateText = BuildAlternateText(file),
                Overwrite = Overwrite,
                Database = Sitecore.Context.Database,
                FileBased = UploadDestination == UploadDestination.File
            }
           .Upload();

            RaiseMediaFileUploadedEvent(mediaUploadResults);

            return FirstStoredFile(file, mediaUploadResults);
        }

        protected byte[] ReadFileBinary(HttpPostedFileBase fileBase)
        {
            var fileBinary = new byte[fileBase.InputStream.Length];
            fileBase.InputStream.Read(fileBinary, 0, (int)fileBase.InputStream.Length);
            return fileBinary;
        }

        protected string BuildAlternateText(HttpPostedFileBase file)
        {
            return file.FileName;
        }

        protected void RaiseMediaFileUploadedEvent(List<MediaUploadResultEx> uploadResults)
        {
            var mediaIds = uploadResults.Select(s => s.Item.ID).ToList();
            var database = Factory.GetDatabase(EventQueueDatabase);


            var args = new MediaUploadEventArgs(mediaIds, Sitecore.Context.Database.Name);            
            Sitecore.Events.Event.RaiseEvent("item:mediafileuploaded", args);
            
            var mediaUploadedEvent = new MediaItemUploadedEventRemote(mediaIds, Sitecore.Context.Database.Name);       
            database.RemoteEvents.EventQueue.QueueEvent(mediaUploadedEvent);

        }

        protected StoredFile FirstStoredFile(HttpPostedFileBase file, List<MediaUploadResultEx> uploadResults)
        {
            return uploadResults.Select(s => new StoredFile
            {
                Url = s.Path,
                OriginalFileName = file.FileName,
                ContentType = file.ContentType,
                ContentLength = file.ContentLength,
                StoredFilePath = s.Path,
                StoredFileName = s.Item.Name
            }).FirstOrDefault();
        }
    }
}