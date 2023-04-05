using ChatGptBackEnd.DataTransferModel;
using ChatGptBackEnd.GptProvider;
using ChatGptBackEnd.MetaModel;
using Microsoft.AspNetCore.Mvc;
using ProgrammerToolkit.Core.Errors;

namespace ProgrammerToolkitBackend.Controllers
{
    [Route("api/gpt")]
    [ApiController]
    public class GptController : ControllerBase
    {
        private IGptMessageProvider _gptMessageProvider;
        public GptController(IGptMessageProvider gptMessageProvider)
        {
            _gptMessageProvider = gptMessageProvider;
        }

        [HttpPost]
        [Route("chat")]
        public async Task<IActionResult> Chat([FromBody] UserMessageRequest request)
        {
            try
            {
                var response = await _gptMessageProvider.SendGptMessage(request);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex);
            }
        }

        [HttpPost]
        [Route("startchat")]
        public async Task<IActionResult> StartChat([FromBody] string content)
        {
            try
            {
                var response = await _gptMessageProvider.SendGptMessage(content, true);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex);
            }
        }

        [HttpPost]
        [Route("consistentchat")]
        public async Task<IActionResult> ConsistentChat([FromBody] string content)
        {
            try
            {
                var response = await _gptMessageProvider.SendGptMessage(content);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex);
            }
        }
    }
}
