using ChatGptBackEnd.DataTransferModel;
using ChatGptBackEnd.GptRepository;
using ChatGptBackEnd.MetaModel;
using ProgrammerToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChatGptBackEnd.GptProvider
{
    public interface IGptMessageProvider
    {
        Task<UserMessageResponse> SendGptMessage(UserMessageRequest message);
    }
    public class GptMessageProvider:IGptMessageProvider
    {
        public readonly ICallGptRepository _callGptRepository;
        public GptMessageProvider(ICallGptRepository callGptRepository)
        {
            _callGptRepository = callGptRepository;
        }
        public async Task<UserMessageResponse> SendGptMessage(UserMessageRequest message)
        {
            message.Role=Role.user.ToString();
            var gptMetaRequest = new GptMetaRequest
            {
                Model = Common.Gpt35Model,
                Messages=new List<GptMessage>()
                {
                    new GptMessage
                    {
                        Role=Role.user.ToString(),
                        Content=message.Message
                    }
                }
            };
            var gptMetaResponse=await _callGptRepository.MakeContent(gptMetaRequest);
            var response = new UserMessageResponse
            {
                Message = gptMetaResponse.Choices[0].Message.Content
            };
            return response;
        }
    }
}
