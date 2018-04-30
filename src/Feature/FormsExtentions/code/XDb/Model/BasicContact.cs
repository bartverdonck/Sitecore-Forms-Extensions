using Sitecore.Globalization;

namespace Feature.FormsExtentions.XDb.Model
{
    public class BasicContact : IBasicContact
    {
        public string IdentifierSource => "email";
        public string IdentifierValue => Email;

        public Language Language { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}