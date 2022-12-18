namespace ProgrammerToolkitBackend.IProvider
{
    public interface IWebToolsProvider
    {
        Task<string> GetWebTools(string token);
    }
}
