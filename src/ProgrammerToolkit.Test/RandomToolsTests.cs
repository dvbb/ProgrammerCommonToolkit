using ProgrammerToolkit.Backend.IProvider;

namespace ProgrammerToolkit.Test
{
    public class RandomToolsTests
    {
        private IRandomToolsProvider _randomToolsProvider;

        public RandomToolsTests()
        {
        }

        public RandomToolsTests(IRandomToolsProvider randomToolsProvider)
        {
            _randomToolsProvider = randomToolsProvider;
        }

        [Test]
        public void GetRandomIpv4Addresses()
        {
            // TODO
        }
    }
}