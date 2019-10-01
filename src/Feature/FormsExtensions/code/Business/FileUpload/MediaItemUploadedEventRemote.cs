using Sitecore.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Feature.FormsExtensions.Business.FileUpload
{
    [DataContract]
    public class MediaItemUploadedEventRemote
    {
        public MediaItemUploadedEventRemote(List<ID> mediaItems, string database)
        {
            MediaItems = mediaItems;
            Database = database;
        }

        [DataMember]
        public List<ID> MediaItems { get; set; }

        [DataMember]
        public string Database { get; set; }
    }
}