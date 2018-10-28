using System;
using System.Web.Script.Serialization;

namespace Feature.FormsExtensions.Business.FileUpload
{
    [Serializable]
    public class StoredFile : IStoredFile
    {
        public string Url { get; set; }
        public string OriginalFileName { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        public string StoredFileName { get; set; }
        public string StoredFilePath { get; set; }

        public override string ToString()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}