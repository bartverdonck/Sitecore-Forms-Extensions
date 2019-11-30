using System.Collections.Generic;
using Sitecore.ExperienceForms.Models;

namespace Feature.FormsExtensions.SubmitActions.SendEmail.FormsField
{
    public interface IFormFieldConverter
    {
        IList<FormField> Convert(IList<IViewModel> postedFields);
    }
}