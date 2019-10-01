using Sitecore.Data;
using System;
using System.Collections.Generic;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaUploadEventArgs : EventArgs
    {
        public List<ID> MediaItems { get; set; }
        public string Database { get; set; }

        public MediaUploadEventArgs(List<ID> mediaItems, string database)
        {
            this.MediaItems = mediaItems;
            this.Database = database;
        }
    }
}