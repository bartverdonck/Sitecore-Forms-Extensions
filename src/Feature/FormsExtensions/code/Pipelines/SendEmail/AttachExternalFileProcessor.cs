using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Sitecore.EDS.Core.Dispatch;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.ExperienceForms.Data;
using Sitecore.ExperienceForms.Data.Entities;
using Sitecore.Modules.EmailCampaign.Messages;

namespace Feature.FormsExtensions.Pipelines.SendEmail
{
    public class AttachExternalFileProcessor
    {
        private readonly IFileStorageProvider fileStorageProvider;

        public AttachExternalFileProcessor(IFileStorageProvider fileStorageProvider)
        {
            this.fileStorageProvider = fileStorageProvider;
        }

        public void Process(SendMessageArgs args)
        {
            if(!(args.EcmMessage is MessageItem ecmmessage))
                return;
            if (!(args.CustomData["EmailMessage"] is EmailMessage message))
            {
                args.AddMessage("Missing EmailMessage from arguments.");
                return;
            }
            foreach (var attachmentReference in ecmmessage.CustomPersonTokens.Keys.Where(k => k.StartsWith("attachment_")))
            {
                var storedFileJSon = ecmmessage.CustomPersonTokens[attachmentReference].ToString();
                var storedFileFromToken = JsonConvert.DeserializeObject<StoredFileInfo>(storedFileJSon);
                var storedFile = fileStorageProvider.GetFile(storedFileFromToken.FileId);
                var fileContent = GetStreamAsByteArray(storedFile.File);
                message.Attachments.Add(new FileResource(storedFile.FileInfo.FileName, fileContent));
            }
        }

        public static byte[] GetStreamAsByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}