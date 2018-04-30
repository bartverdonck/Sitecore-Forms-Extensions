using System.Collections.Generic;
using Sitecore.ExperienceForms.Models;

namespace Feature.FormsExtentions.Business
{
    public interface IFormFieldConverter
    {
        IList<FormField> Convert(IList<IViewModel> postedFields);
    }
}