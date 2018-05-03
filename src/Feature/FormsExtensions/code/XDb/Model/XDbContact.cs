namespace Feature.FormsExtensions.XDb.Model
{
    public abstract class XDbContact : IXDbContact
    {
        public string IdentifierSource => "email";
        public string IdentifierValue => Email;
        public string Email { get; set; }
    }
}