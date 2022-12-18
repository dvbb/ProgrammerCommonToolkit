using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammerToolkitBackend.IProvider;
using ProgrammerToolkitBackend.Provider;

namespace ProgrammerToolkitBackend.Controllers
{
    [Route("api/webtool")]
    [ApiController]
    public class WebToolsController : ControllerBase
    {
        public IWebToolsProvider _webToolsProvider;
        public WebToolsController(IWebToolsProvider webToolsProvider)
        {
            _webToolsProvider= webToolsProvider;
        }

        [HttpPost("decodejwt")]
        public async Task<IActionResult> DecodeJwtToken(string token)
        {
            var claims = await _webToolsProvider.GetWebTools(token);
            return StatusCode(StatusCodes.Status200OK, claims);
        }
    }
}
