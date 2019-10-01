using Sitecore.IO;
using Sitecore.Resources.Media;
using System;
using System.IO;

namespace Feature.FormsExtensions.Business.FileUpload
{
    [Serializable]
    public class PostedFile
    {
        public PostedFile()
        {
        }

        public PostedFile(byte[] data, string fileName, string destination) : this()
        {
            Data = data;
            FileName = fileName;
            Destination = destination;
        }

        public byte[] Data { get; set; }

        public string Destination { get; set; }

        public string FileName { get; set; }

        public Stream GetInputStream()
        {
            var memoryStream = new MemoryStream();
            memoryStream.Write(Data, 0, Data.Length);
            return memoryStream;
        }

        public void SaveAs(string filename)
        {
            using (var fileStream = new FileStream(filename, FileMode.Create))
            {
                fileStream.Write(Data, 0, Data.Length);
            }
        }

        public override string ToString()
        {
            return MediaPathManager.ProposeValidMediaPath(FileUtil.MakePath(Destination, Path.GetFileName(FileName), '/'));
        }
    }
}