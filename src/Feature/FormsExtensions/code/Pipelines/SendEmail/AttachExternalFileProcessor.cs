using System.Linq;
using Feature.FormsExtensions.Business.FileUpload;
using Newtonsoft.Json;
using Sitecore.EDS.Core.Dispatch;
using Sitecore.EmailCampaign.Cm.Pipelines.SendEmail;
using Sitecore.Modules.EmailCampaign.Messages;

namespace Feature.FormsExtensions.Pipelines.SendEmail
{
    public class AttachExternalFileProcessor
    {

        private readonly IFileUploadStorageProviderFactory fileUploadStorageProviderFactory;

        public AttachExternalFileProcessor(IFileUploadStorageProviderFactory fileUploadStorageProviderFactory)
        {
            this.fileUploadStorageProviderFactory = fileUploadStorageProviderFactory;
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
            foreach (var attachmentReference in ecmmessage.CustomPersonTokens.Keys.Where(k =>
                k.StartsWith("attachment_")))
            {
                var storedFileJson = ecmmessage.CustomPersonTokens[attachmentReference].ToString();
                var storedFile = JsonConvert.DeserializeObject<StoredFile>(storedFileJson);
                var fileUploadStorageProvider = fileUploadStorageProviderFactory.GetDefaultFileUploadStorageProvider();
                var fileContent = fileUploadStorageProvider.GetFileAsBytes(storedFile);
                message.Attachments.Add(new FileResource(storedFile.OriginalFileName, fileContent));
            }
        }
    }
}