using Sitecore.Globalization;

namespace Feature.FormsExtensions.XDb.Model
{
    public interface IBasicContact : IXDbContact
    {
        Language Language { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
