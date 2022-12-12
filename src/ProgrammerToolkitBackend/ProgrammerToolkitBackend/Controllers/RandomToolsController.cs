using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammerToolkitBackend.Controllers
{
    [ApiController]
    public class RandomToolsController : ControllerBase
    {
        [HttpGet]
        [Route("api/RandonTools/Ipv4/{count}")]
        public List<string> GetRandomIpv4Address(int count = 1)
        {
            List<string> list = new List<string>();
            // todo
            return list;
        }

        [HttpGet]
        [Route("api/RandonTools/Ipv6/{count}")]
        public List<string> GetRandomIpv6Address(int count = 1)
        {
            List<string> list = new List<string>();
            // todo
            return list;
        }

        [HttpGet]
        [Route("api/RandonTools/Password/{count}")]
        public List<string> GetRandomPassword(int count = 1)
        {
            List<string> list = new List<string>();
            // todo
            return list;
        }
    }
}
