using ProgrammerToolkit.Backend.IProvider;

namespace ProgrammerToolkitBackend.Provider
{
    public class RandomToolsProvider : IRandomToolsProvider
    {
        private readonly Random _random = new Random();

        public async Task<List<string>> GetRandomIpv4Addresses(int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(GetRandomIpv4Address());
            }
            return list;
        }

        private string GetRandomIpv4Address() => $"{_random.Next(255)}.{_random.Next(255)}.{_random.Next(255)}.{_random.Next(255)}";

        public async Task<List<string>> GetRandomIpv6Addresses(int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(GetRandomIpv6Address());
            }
            return list;
        }

        private string GetRandomIpv6Address() => $"{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}";

        public async Task<List<string>> GetRandomPasswords(int count)
        {
            var list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(GetRandomPassword());
            }
            return list;
        }

        private string GetRandomPassword()
        {
            int length = _random.Next(8,16);
            string password = "";
            for (int i = 0; i < length; i++)
            {
                password += (Char)_random.Next(33,125);
            }
            return password;
        }
    }
}
