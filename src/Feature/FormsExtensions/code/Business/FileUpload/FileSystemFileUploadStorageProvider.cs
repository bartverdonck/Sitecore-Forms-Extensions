using System;
using System.IO;
using System.Web;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class FileSystemFileUploadStorageProvider : IFileUploadStorageProvider
    {
        public string RootStoragePath { get; set; }
        public string FileDownloadUrlBase { get; set; }

        public IStoredFile StoreFile(HttpPostedFileBase fileBase)
        {
            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileBase.FileName)}";
            var path = Path.Combine(RootStoragePath, fileName);
            fileBase.SaveAs(path);
            var storedFile = new StoredFile
            {
                Url = string.Format(FileDownloadUrlBase,fileName),
                OriginalFileName = fileBase.FileName,
                ContentType = fileBase.ContentType,
                ContentLength = fileBase.ContentLength
            };
            return storedFile;
        }
    }
}