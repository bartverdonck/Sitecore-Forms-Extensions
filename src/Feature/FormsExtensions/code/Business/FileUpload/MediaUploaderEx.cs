using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Pipelines.GetMediaCreatorOptions;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using Sitecore.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaUploaderEx : MediaUploader
    {
        public new PostedFile File { get; set; }

        public new List<MediaUploadResultEx> Upload()
        {
            using (new SecurityDisabler())
            {
                var uploadResults = new List<MediaUploadResultEx>();

                var isZipFile = string.Compare(Path.GetExtension(File.FileName), ".zip", StringComparison.OrdinalIgnoreCase) == 0 && Unpack;
                if (isZipFile)
                {
                    UnpackToDatabase(uploadResults);
                    return uploadResults;
                }

                UploadToDatabase(uploadResults);
                return uploadResults;
            }
        }

        protected void UnpackToDatabase(List<MediaUploadResultEx> uploadResults)
        {
            var tempPath = FileUtil.MapPath(TempFolder.GetFilename("temp.zip"));
            File.SaveAs(tempPath);

            try
            {
                using (var zipReader = new ZipReader(tempPath))
                {
                    foreach (var file in zipReader.Entries)
                    {
                        if (file.IsDirectory) continue;

                        UploadFileAndUpdateUploadResults(file, uploadResults);
                    }
                }
            }
            finally
            {
                FileUtil.Delete(tempPath);
            }
        }

        private void UploadFileAndUpdateUploadResults(ZipEntry file, List<MediaUploadResultEx> uploadResults)
        {
            var mediaUploadResult = new MediaUploadResultEx();
            uploadResults.Add(mediaUploadResult);

            mediaUploadResult.Path = FileUtil.MakePath(Folder, file.Name, '/'); //

            mediaUploadResult.ValidMediaPath = MediaPathManager.ProposeValidMediaPath(mediaUploadResult.Path);
            var mediaCreatorOption = GetDefaultMediaCreatorOptions(mediaUploadResult.ValidMediaPath);

            mediaCreatorOption.Build(GetMediaCreatorOptionsArgs.UploadContext);

            var stream = file.GetStream();

            mediaUploadResult.Item = MediaManager.Creator.CreateFromStream(stream, mediaUploadResult.Path, mediaCreatorOption);
        }

        private MediaCreatorOptions GetDefaultMediaCreatorOptions(string destination)
        {
            return new MediaCreatorOptions
            {
                Versioned = Versioned,
                Language = Language,
                OverwriteExisting = Overwrite,
                Destination = destination,
                FileBased = FileBased,
                AlternateText = AlternateText,
                Database = Database
            };
        }

        protected void UploadToDatabase(List<MediaUploadResultEx> list)
        {
            Assert.ArgumentNotNull(list, "list");

            if (Context.Database == null) Context.Database = Database;

            var mediaUploadResult = new MediaUploadResultEx();
            list.Add(mediaUploadResult);

            mediaUploadResult.Path = FileUtil.MakePath(Folder, Path.GetFileName(File.FileName), '/');

            mediaUploadResult.ValidMediaPath = MediaPathManager.ProposeValidMediaPath(mediaUploadResult.Path);
            var mediaCreatorOption = GetDefaultMediaCreatorOptions(mediaUploadResult.ValidMediaPath);

            mediaCreatorOption.Build(GetMediaCreatorOptionsArgs.UploadContext);

            mediaUploadResult.Item = MediaManager.Creator.CreateFromStream(File.GetInputStream(), mediaUploadResult.Path, mediaCreatorOption);
        }
    }
}