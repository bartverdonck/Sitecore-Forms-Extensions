namespace Feature.FormsExtensions.XDb.Model
{
    public interface IXDbContact
    {
        string IdentifierSource { get; }
        string IdentifierValue { get; }
    }

    public interface IXDbContactWithEmail : IXDbContact
    {
        string Email { get; }
    }
}