using Sitecore.ExperienceForms.Models;

namespace Feature.FormsExtensions.Fields.ValueResolvers
{
    public interface IFormsFieldValueResolver
    {
        string GetStringFieldValue(IViewModel fieldViewModel);
    }
}