namespace ProgrammerToolkitBackend.IProvider
{
    public interface IWebToolsProvider
    {
        Task<string> DecodeJwtToken(string token);
    }
}
