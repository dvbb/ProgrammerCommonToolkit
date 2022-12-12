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
    }
}
