using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammerToolkit.Core;
using ProgrammerToolkitBackend.Provider;

namespace ProgrammerToolkitBackend.Controllers
{
    [ApiController]
    public class RandomToolsController : ControllerBase
    {
        public RandomToolsProvider _randomToolsProvider => new RandomToolsProvider();

        [HttpGet]
        [Route("api/RandonTools/Ipv4/{count}")]
        public List<string> GetRandomIpv4Address(int count = 1)
        {
            ParameterValidator.ValidatePositiveNumber(count);
            List<string> list = _randomToolsProvider.GetRandomIpv4Addresses(count);
            return list;
        }

        [HttpGet]
        [Route("api/RandonTools/Ipv6/{count}")]
        public List<string> GetRandomIpv6Address(int count = 1)
        {
            ParameterValidator.ValidatePositiveNumber(count);
            List<string> list = new List<string>();
            // todo
            return list;
        }

        [HttpGet]
        [Route("api/RandonTools/Password/{count}")]
        public List<string> GetRandomPassword(int count = 1)
        {
            ParameterValidator.ValidatePositiveNumber(count);
            List<string> list = new List<string>();
            // todo
            return list;
        }
    }
}
