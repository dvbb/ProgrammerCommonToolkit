using ProgrammerToolkit.Backend.IProvider;

namespace ProgrammerToolkit.Test
{
    public class RandomToolsTests
    {
        private IRandomToolsProvider _randomToolsProvider;
        
        public RandomToolsTests(IRandomToolsProvider randomToolsProvider)
        {
            _randomToolsProvider = randomToolsProvider;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetRandomIpv4Addresses()
        {
            // TODO
        }
    }
}