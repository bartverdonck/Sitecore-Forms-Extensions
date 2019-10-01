using System;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaLibraryFolderBuilder
    {
        private const string MediaLibraryPath = "/sitecore/media library";

        public static string BuildFolder(BuildMediaLibraryFolderParameter parameter)
        {
            if (string.IsNullOrEmpty(parameter.Path))
                return string.Empty;

            var formItem = GetFormItem(parameter.FormId);

            if (formItem == null)
                return string.Empty;

            var filePath = $"{parameter.Path.TrimEnd('/')}/{formItem.Name}/{formItem.Language.Name}";
            var folders = TrimMediaLibraryPath(filePath).Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            using (new SecurityDisabler())
            {
                var builtPath = MediaLibraryPath;
                var database = Sitecore.Context.Database;

                foreach (var folder in folders)
                {
                    var folderItem = database.GetItem(builtPath.TrimEnd('/') + "/" + folder);
                    if (folderItem == null)
                    {
                        folderItem = database.GetItem(builtPath);
                        folderItem.Add(folder, new TemplateID(TemplateIDs.MediaFolder));
                    }

                    builtPath = builtPath.TrimEnd('/') + "/" + folder;
                }
            }

            return filePath;
        }

        protected static string TrimMediaLibraryPath(string path)
        {
            return path.Substring(MediaLibraryPath.Length);
        }

        protected static Item GetFormItem(string formId)
        {
            var database = Sitecore.Context.Database;
            var formItem = database.GetItem(formId);
            return formItem;
        }
    }
}