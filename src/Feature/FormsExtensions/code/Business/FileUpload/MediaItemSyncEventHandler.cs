using Feature.FormsExtensions.Helpers;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using System;
using System.Linq;

namespace Feature.FormsExtensions.Business.FileUpload
{
    public class MediaItemSyncEventHandler
    {
        public string TargetDatabase { get; set; }

        public MediaItemSyncEventHandler()
        {

        }

        public virtual void SubscribeToRemoteEvent(PipelineArgs args)
        {
            Assert.ArgumentNotNullOrEmpty(TargetDatabase, "TargetDatabase");
            var action = new Action<MediaItemUploadedEventRemote>(OnFileUploadedRemote);
            Sitecore.Eventing.EventManager.Subscribe(action);
        }

        protected virtual void OnFileUploadedRemote(MediaItemUploadedEventRemote uploadEvent)
        {
            try
            {
                var args = new MediaUploadEventArgs(uploadEvent.MediaItems, uploadEvent.Database);
                ProcessItemDatabaseTransfer(args);
            }
            catch (Exception ex)
            {
                Log.Error("[MediaItemSyncEventHandler]: An exception occurred while transfering item between databases", ex, this);
            }
        }

        protected void OnFileUploaded(object sender, EventArgs args)
        {
            try
            {
                var mediaUploadEventArgs = GetMediaUploadEventArgs(args);
                Assert.ArgumentNotNull(mediaUploadEventArgs, "mediaUploadEventArgs");

                ProcessItemDatabaseTransfer(mediaUploadEventArgs);
            }
            catch (Exception ex)
            {
                Log.Error("[MediaItemSyncEventHandler]: An exception occurred while transfering item between databases", ex, this);
            }
        }

        private MediaUploadEventArgs GetMediaUploadEventArgs(EventArgs args)
        {
            var sitecoreEventArgs = args as Sitecore.Events.SitecoreEventArgs;
            var mediaUploadEventArgs = sitecoreEventArgs?.Parameters?.FirstOrDefault() as MediaUploadEventArgs;
            return mediaUploadEventArgs;
        }

        protected void ProcessItemDatabaseTransfer(MediaUploadEventArgs args)
        {
            var sourceDatabase = Sitecore.Configuration.Factory.GetDatabase(args.Database);
            foreach (var mediaId in args.MediaItems)
            {
                var item = sourceDatabase?.GetItem(mediaId);
                if (item != null)
                {
                    ItemTransferHelper itemTransfer = new ItemTransferHelper(TargetDatabase, item);
                    itemTransfer.TransferItem();
                }
            }
        }
    }
}