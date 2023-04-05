using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammerToolkit.Backend.IProvider;
using ProgrammerToolkit.Core;
using ProgrammerToolkit.Core.Errors;
using ProgrammerToolkitBackend.IProvider;
using ProgrammerToolkitBackend.Provider;
using System.Diagnostics;

namespace ProgrammerToolkitBackend.Controllers
{
    [Route("api/randomtool")]
    [ApiController]
    public class RandomToolsController : ControllerBase
    {
        private IRandomToolsProvider _randomToolsProvider;

        public RandomToolsController(IRandomToolsProvider randomToolsProvider)
        {
            _randomToolsProvider = randomToolsProvider;
        }

        [HttpGet]
        [Route("api/RandonTools/Ipv4/{count}")]
        public async Task<List<string>> GetRandomIpv4Address(int count = 1)
        {
            ParameterValidator.ValidatePositiveNumber(count);
            var list = await _randomToolsProvider.GetRandomIpv4Addresses(count);
            return list;
        }

        [HttpGet]
        [Route("api/RandonTools/Ipv6/{count}")]
        public async Task<List<string>> GetRandomIpv6Address(int count = 1)
        {
            ParameterValidator.ValidatePositiveNumber(count);
            var list = await _randomToolsProvider.GetRandomIpv6Addresses(count);
            return list;
        }

        [HttpGet]
        [Route("api/RandonTools/Password/{count}")]
        public async Task<List<string>> GetRandomPassword(int count = 1)
        {
            ParameterValidator.ValidatePositiveNumber(count);
            var list = await _randomToolsProvider.GetRandomPasswords(count);
            return list;
        }
    }
}
