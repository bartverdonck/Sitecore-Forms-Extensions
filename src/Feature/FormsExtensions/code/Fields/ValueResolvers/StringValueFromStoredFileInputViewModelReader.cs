using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.ExperienceForms.Data.Entities;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Mvc.Pipelines;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public class StringValueFromStoredFileInputViewModelReader : MvcPipelineProcessor<GetStringValueFromViewModelArgs>
    {
        public override void Process(GetStringValueFromViewModelArgs args)
        {
            if (args.FieldViewModel is InputViewModel<List<StoredFileInfo>> fileInputViewModel)
            {
                if (fileInputViewModel.Value != null && fileInputViewModel.Value.Count > 0)
                {
                    args.Value = fileInputViewModel.Value.Aggregate("",
                        (current, value) => current + ", " + FormatToDownloadLink(value.FileId.ToString())).Remove(0, 2);
                }
                else
                {
                    args.Value = string.Empty;
                }
                args.AbortPipeline();
            }
        }

        private static string FormatToDownloadLink(string fileId)
        {
            return Globals.ServerUrl + "/sitecore/api/ssc/forms/formdata/formdata/DownloadFile?fileId=" + fileId;
        }
    }
}