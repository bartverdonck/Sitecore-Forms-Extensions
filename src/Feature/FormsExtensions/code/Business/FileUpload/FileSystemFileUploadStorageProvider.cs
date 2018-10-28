using System;
using System.IO;
using Feature.FormsExtensions.Fields.FileUpload;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class FileSystemFileUploadStorageProvider : IFileUploadStorageProvider
    {
        public string RootStoragePath { get; set; }
        public string FileDownloadUrlBase { get; set; }
        public string Folder { get; set; }
        
        public IStoredFile StoreFile(FileUploadModel fileUploadModel, Guid formId)
        {
            var folder = FolderBuilder.BuildFolder(Folder, fileUploadModel, formId);
            Directory.CreateDirectory(Path.Combine(RootStoragePath, folder));
            var fileBase = fileUploadModel.File;
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileBase.FileName)}";
            var path = Path.Combine(RootStoragePath, folder, fileName);
            fileBase.SaveAs(path);
            var storedFile = new StoredFile
            {
                Url = string.Format(FileDownloadUrlBase, fileName),
                OriginalFileName = fileBase.FileName,
                ContentType = fileBase.ContentType,
                ContentLength = fileBase.ContentLength,
                StoredFilePath = folder,
                StoredFileName = fileName
            };
            return storedFile;
        }

        public byte[] GetFileAsBytes(IStoredFile storedFile)
        {
            var path = Path.Combine(RootStoragePath, storedFile.StoredFilePath, storedFile.StoredFileName);
            return File.ReadAllBytes(path);
        }
    }
}