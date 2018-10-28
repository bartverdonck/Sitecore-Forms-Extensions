using System;
using System.Web;
using Feature.FormsExtensions.Fields.FileUpload;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public interface IFileUploadStorageProvider
    {
        IStoredFile StoreFile(FileUploadModel fileUploadModel, Guid formId);
        byte[] GetFileAsBytes(IStoredFile storedFile);
    }
}