using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Feature.FormsExtensions.Helpers
{
    public class ItemTransferHelper
    {
        private const int MaxBlobFieldLength = 38;

        private readonly Database _destinationDatabase;
        private readonly Database _sourceDatabase;

        private readonly Item _source;

        public ItemTransferHelper(string destinationDatabaseName, Item sourceItem)
        {
            Assert.ArgumentNotNullOrEmpty(destinationDatabaseName, "destinationDatabaseName");
            Assert.ArgumentNotNull(sourceItem, "sourceItem");

            this._destinationDatabase = Factory.GetDatabase(destinationDatabaseName);
            this._source = sourceItem;
            this._sourceDatabase = sourceItem.Database;
        }

        public void TransferItem()
        {
            EnsurePath(_source);
            TransferItem(_source, true);
        }

        protected void EnsurePath(Item item)
        {
            foreach (Item ancestor in item.Axes.GetAncestors())
            {
                if (_destinationDatabase.GetItem(ancestor.ID) == null)
                    TransferItem(ancestor, false);
            }
        }

        protected void TransferItem(Item source, bool deep)
        {
            Item destination = _destinationDatabase.GetItem(source.Parent.ID);
            TransferItem(source, destination, deep);
        }

        protected void TransferItem(Item source, Item destination, bool deep)
        {
            using (new SecurityDisabler())
            {
                var defaultOptions = GetDefaultItemSerializerOptions();
                defaultOptions.ProcessChildren = deep;

                string outerXml = source.GetOuterXml(defaultOptions);
                destination.Paste(outerXml, false, PasteMode.Overwrite);

                if (source.Paths.IsMediaItem)
                {
                    TransferMediaItem(source, destination, deep);
                }
                Sitecore.Diagnostics.Log.Audit(this, $"[Item Transfer]: Transfer from {0} to {1}. Deep: {2}", new[] { AuditFormatter.FormatItem(source), AuditFormatter.FormatItem(destination), deep.ToString() });
            }
        }

        private ItemSerializerOptions GetDefaultItemSerializerOptions()
        {
            ItemSerializerOptions defaultOptions = ItemSerializerOptions.GetDefaultOptions();
            defaultOptions.AllowDefaultValues = false;
            defaultOptions.AllowStandardValues = false;
            defaultOptions.IncludeBlobFields = true;
            return defaultOptions;
        }

        protected void TransferMediaItem(Item source, Item destination, bool deep)
        {
            if (source.TemplateID != TemplateIDs.MediaFolder && WebDAVItemUtil.IsVersioned(source))
                this.TransferVersionedMediaItemBlob(source, destination, deep);
            else
                this.TransferMediaItemBlob(source, destination, deep);
        }

        protected void TransferVersionedMediaItemBlob(Item source, Item destination, bool processChildren)
        {
            Parallel.ForEach<Language>((IEnumerable<Language>)source.Languages, (System.Action<Language>)(language =>
            {
                Item item = source.Database.GetItem(source.ID, language);
                if (item == null || item.Versions.Count <= 0)
                    return;

                VersionCollection versions = ItemManager.GetVersions(item);
                if (versions == null)
                    return;

                foreach (Sitecore.Data.Version version in (List<Sitecore.Data.Version>)versions)
                {
                    Item itemVersion = item.Database.GetItem(source.ID, language, version);
                    if (itemVersion != null)
                        this.TransferMediaItemBlob(itemVersion, destination, processChildren);
                }
            }));
        }

        protected void TransferMediaItemBlob(Item source, Item destination, bool processChildren)
        {
            foreach (Field field in source.Fields)
            {
                if (field.IsBlobField)
                {
                    TransferFieldBlobStream(field);
                }
            }

            if (processChildren)
            {
                ProcessChildrenMediaItemBlob(source, destination, processChildren);
            }
        }

        protected void TransferFieldBlobStream(Field field)
        {
            var blobFieldGuid = GetBlobFieldGuid(field);
            if (!(blobFieldGuid == Guid.Empty))
            {
                Stream blobStream = ItemManager.GetBlobStream(blobFieldGuid, this._sourceDatabase);
                if (blobStream != null)
                {
                    using (blobStream)
                    {
                        ItemManager.SetBlobStream(blobStream, blobFieldGuid, this._destinationDatabase);
                    }
                }
            }
        }

        private Guid GetBlobFieldGuid(Field field)
        {
            string fieldValue = field.Value;
            if (fieldValue.Length > MaxBlobFieldLength)
            {
                fieldValue = fieldValue.Substring(0, MaxBlobFieldLength);
            }
            Guid guid = MainUtil.GetGuid((object)fieldValue, Guid.Empty);
            return guid;
        }

        protected void ProcessChildrenMediaItemBlob(Item source, Item destination, bool processChildren)
        {
            foreach (Item child in source.Children)
            {
                if (child != null)
                {
                    if (child.TemplateID != TemplateIDs.MediaFolder && WebDAVItemUtil.IsVersioned(child))
                        this.TransferVersionedMediaItemBlob(child, destination, processChildren);
                    else
                        this.TransferMediaItemBlob(child, destination, processChildren);
                }
            }
        }
    }
}