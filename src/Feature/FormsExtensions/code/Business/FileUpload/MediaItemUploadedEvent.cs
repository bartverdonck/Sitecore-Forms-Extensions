using Sitecore.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Feature.FormsExtensions.Business.FileUpload
{
    [DataContract]
    public class MediaItemUploadedEvent
    {
        [DataMember]
        public List<ID> MediaItems { get; set; }

        [DataMember]
        public string Database { get; set; }
    }
}