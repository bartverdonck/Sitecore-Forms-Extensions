using Feature.FormsExtensions.Business.FileUpload;
using Feature.FormsExtensions.Helpers;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using System;

namespace Feature.FormsExtensions.Pipelines.Initialize
{
    public class MediaItemSyncEventHandler
    {
        public string TargetDatabase { get; set; }

        public virtual void InitializeFromPipeline(PipelineArgs args)
        {
            Assert.ArgumentNotNullOrEmpty(this.TargetDatabase, "TargetDatabase");

            var action = new Action<MediaItemUploadedEvent>(RaiseRemoteEvent);
            Sitecore.Eventing.EventManager.Subscribe<MediaItemUploadedEvent>(action);
        }

        protected void RaiseRemoteEvent(MediaItemUploadedEvent mediaUploadEvent)
        {
            try
            {
                ProcessItemDatabaseTransfer(mediaUploadEvent);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("[Media item uploaded event handler]: An exception occurred while transfering item between databases", ex, this);
            }
        }

        protected void ProcessItemDatabaseTransfer(MediaItemUploadedEvent mediaUploadEvent)
        {
            var sourceDatabase = Sitecore.Configuration.Factory.GetDatabase(mediaUploadEvent.Database);
            foreach (var mediaId in mediaUploadEvent.MediaItems)
            {
                var item = sourceDatabase?.GetItem(mediaId);
                if (item != null)
                {
                    ItemTransferHelper itemTransfer = new ItemTransferHelper(this.TargetDatabase, item);
                    itemTransfer.TransferItem();
                }
            }
        }
    }
}