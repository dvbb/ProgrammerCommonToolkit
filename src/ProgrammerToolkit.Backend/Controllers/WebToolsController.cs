using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json.Linq;
using ProgrammerToolkit.Core.Errors;
using ProgrammerToolkitBackend.IProvider;
using ProgrammerToolkitBackend.Provider;
using ProgrammerToolkitBackend.Response;
using System.Security.Claims;

namespace ProgrammerToolkitBackend.Controllers
{
    [Route("api/webtool")]
    [ApiController]
    public class WebToolsController : ControllerBase
    {
        private IWebToolsProvider _webToolsProvider;
        private IErrorMap _errorMap;
        public WebToolsController(IWebToolsProvider webToolsProvider,
                                    IErrorMap errorMap)
        {
            _webToolsProvider= webToolsProvider;
            _errorMap= errorMap;
        }

        [Route("decodejwt"),HttpPost]
        public async Task<IActionResult> DecodeJwtToken(
            [FromBody]string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var errorResponse = _errorMap.CreateErrorResponse(ErrorCode.JWT_NULL_ERROR);
                return BadRequest(errorResponse);
            }
            try
            {
                var claims = await _webToolsProvider.DecodeJwtToken(token);
                return StatusCode(StatusCodes.Status200OK, claims);
            }
            catch(Exception ex)
            {
                var errorResponse = _errorMap.CreateErrorResponse(ErrorCode.JWT_DECODE_ERROR);
                return StatusCode(StatusCodes.Status409Conflict, errorResponse);
            }
        }
        [Route("encodejwt"), HttpPost]
        public async Task<IActionResult> EncodeJwtToken(
            [FromBody]string jsonClaims)
        {
            string alg = "";
            if (string.IsNullOrEmpty(jsonClaims)|| string.IsNullOrEmpty(alg))
            {
                var errorResponse = _errorMap.CreateErrorResponse(ErrorCode.JWT_NULL_ERROR);
                return BadRequest(errorResponse);
            }
            return StatusCode(StatusCodes.Status200OK, "");

        }
    }
}
