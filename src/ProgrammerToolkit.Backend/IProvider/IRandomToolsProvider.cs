namespace ProgrammerToolkit.Backend.IProvider
{
    public interface IRandomToolsProvider
    {
        Task<List<string>> GetRandomIpv4Addresses(int count);
        Task<List<string>> GetRandomIpv6Addresses(int count);
        Task<List<string>> GetRandomPasswords(int count);
    }
}
