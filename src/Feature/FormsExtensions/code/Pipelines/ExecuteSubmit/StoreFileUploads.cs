using Feature.FormsExtensions.Business.FileUpload;
using Feature.FormsExtensions.Fields.FileUpload;
using Sitecore.ExperienceForms.Mvc.Pipelines.ExecuteSubmit;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Pipelines.ExecuteSubmit
{
    public class StoreFileUploads : MvcPipelineProcessor<ExecuteSubmitActionsEventArgs>
    {
        private readonly IFileUploadStorageProvider fileUploadStorageProvider;

        public StoreFileUploads(IFileUploadStorageProvider fileUploadStorageProvider)
        {
            this.fileUploadStorageProvider = fileUploadStorageProvider;
        }

        public override void Process(ExecuteSubmitActionsEventArgs args)
        {
            foreach (var field in args.FormSubmitContext.Fields)
            {
                var uploadField = field as FileUploadModel;
                if (uploadField == null)
                    continue;
                HandleUploadField(uploadField);
            }
        }

        private void HandleUploadField(FileUploadModel uploadField)
        {
            if (uploadField.File == null)
                return;
            var storedFile = fileUploadStorageProvider.StoreFile(uploadField.File);
            uploadField.Value = storedFile;
        }
        
    }
}