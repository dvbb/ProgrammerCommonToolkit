namespace ProgrammerToolkitBackend.Provider
{
    public class RandomToolsProvider
    {
        private readonly Random _random = new Random();

        public List<string> GetRandomIpv4Addresses(int count)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(GetRandomIpv4Address());
            }
            return list;
        }

        private string GetRandomIpv4Address() => $"{_random.Next(255)}.{_random.Next(255)}.{_random.Next(255)}.{_random.Next(255)}";

        public List<string> GetRandomIpv6Addresses(int count)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < count; i++)
            {
                list.Add(GetRandomIpv6Address());
            }
            return list;
        }

        private string GetRandomIpv6Address() => $"{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}:{_random.Next(65536).ToString("X")}";

        public List<string> GetRandomPasswords(int count)
        {
            List<string> list = new List<string>();
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
