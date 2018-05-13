using System;
using System.IO;
using System.Web;
using Feature.FormsExtensions.Fields.FileUpload;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class FileSystemFileUploadStorageProvider : IFileUploadStorageProvider
    {
        public IStoredFile StoreFile(HttpPostedFileBase fileBase)
        {
            var fileName = $"{Guid.NewGuid().ToString()}.{Path.GetExtension(fileBase.FileName)}";
            var path = Path.Combine(@"c:\temp\", fileName);
            fileBase.SaveAs(path);
            var storedFile = new StoredFile
            {
                Url = $"http://jos/{fileName}",
                OriginalFileName = fileBase.FileName,
                ContentType = fileBase.ContentType,
                ContentLength = fileBase.ContentLength
            };
            return storedFile;
        }
    }
}