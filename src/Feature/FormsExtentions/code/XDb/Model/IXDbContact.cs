namespace Feature.FormsExtentions.XDb.Model
{
    public interface IXDbContact
    {
        string IdentifierSource { get; }
        string IdentifierValue { get; }
        string Email { get; }
    }
}