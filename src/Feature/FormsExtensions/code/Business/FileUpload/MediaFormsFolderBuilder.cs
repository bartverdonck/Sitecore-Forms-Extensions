using System;
using Feature.FormsExtensions.Fields.FileUpload;
using Sitecore.Data;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaFormsFolderBuilder
    {
        public static string BuildFolder(string rawFolder, FileUploadModel fileUploadModel, Guid formId)
        {
            if (rawFolder == null)
                return "";
            var folder = rawFolder;
            if (folder.Contains("{formName}"))
            {
                var formItem = Sitecore.Context.Database.GetItem(new ID(formId));
                folder = folder.Replace("{formName}", formItem.Name);
            }
            if (folder.Contains("{fieldName}"))
            {
                folder = folder.Replace("{fieldName}", fileUploadModel.Name);
            }
            if (folder.Contains("{language}"))
            {
                folder = folder.Replace("{language}", Sitecore.Context.Language.Name);
            }
            return folder;
        }
    }
}