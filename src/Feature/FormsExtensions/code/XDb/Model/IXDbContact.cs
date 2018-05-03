namespace Feature.FormsExtensions.XDb.Model
{
    public interface IXDbContact
    {
        string IdentifierSource { get; }
        string IdentifierValue { get; }
        string Email { get; }
    }
}