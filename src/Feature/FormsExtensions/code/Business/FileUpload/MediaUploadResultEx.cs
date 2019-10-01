using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaUploadResultEx
    {
        private Item _item;

        private string _path;

        private string _validMediaPath;

        public Item Item
        {
            get => _item;
            internal set
            {
                Assert.ArgumentNotNull(value, "value");
                _item = value;
            }
        }

        public string Path
        {
            get => _path;
            internal set
            {
                Assert.ArgumentNotNull(value, "value");
                _path = value;
            }
        }

        public string ValidMediaPath
        {
            get => _validMediaPath;
            internal set
            {
                Assert.ArgumentNotNull(value, "value");
                _validMediaPath = value;
            }
        }
    
    }
}