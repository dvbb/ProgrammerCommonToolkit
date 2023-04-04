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
        private GptMetaRequest _sessionMessage;
        public GptController(IGptMessageProvider gptMessageProvider)
        {
            _gptMessageProvider = gptMessageProvider;
            _sessionMessage = new GptMetaRequest();
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
                _sessionMessage = new GptMetaRequest();
                _sessionMessage = await _gptMessageProvider.SendGptMessage(_sessionMessage, content);
                return StatusCode(StatusCodes.Status200OK, _sessionMessage.Messages.Last().Content);
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
                _sessionMessage = await _gptMessageProvider.SendGptMessage(_sessionMessage, content);
                return StatusCode(StatusCodes.Status200OK, _sessionMessage.Messages.Last().Content);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status409Conflict, ex);
            }
        }
    }
}
