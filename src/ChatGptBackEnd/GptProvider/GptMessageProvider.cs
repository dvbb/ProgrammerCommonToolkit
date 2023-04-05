using ChatGptBackEnd.DataTransferModel;
using ChatGptBackEnd.GptRepository;
using ChatGptBackEnd.MetaModel;
using Newtonsoft.Json;
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
        Task<UserMessageResponse> SendGptMessage(string content, bool isNewSession = false);
    }
    public class GptMessageProvider : IGptMessageProvider
    {
        public readonly ICallGptRepository _callGptRepository;
        private GptMetaRequest _sessionMessage;
        public GptMessageProvider(ICallGptRepository callGptRepository)
        {
            _callGptRepository = callGptRepository;
            _sessionMessage = new GptMetaRequest();
        }
        public async Task<UserMessageResponse> SendGptMessage(UserMessageRequest message)
        {
            message.Role = Role.user.ToString();
            var gptMetaRequest = new GptMetaRequest
            {
                Model = Common.Gpt35Model,
                Messages = new List<GptMessage>()
                {
                    new GptMessage
                    {
                        Role=Role.user.ToString(),
                        Content=message.Message
                    }
                }
            };
            var gptMetaResponse = await _callGptRepository.MakeContent(gptMetaRequest);
            var response = new UserMessageResponse
            {
                Message = gptMetaResponse.Choices[0].Message.Content
            };
            return response;
        }

        public async Task<UserMessageResponse> SendGptMessage(string content, bool isNewSession = false)
        {
            if (isNewSession)
            {
                _sessionMessage = new GptMetaRequest();
            }
            GptMessage userMessage = new GptMessage()
            {
                Role = Role.user.ToString(),
                Content = content
            };
            _sessionMessage.Messages.Add(userMessage);

            // send a http request to GPT server
            using (var client = new HttpClient())
            {
                var apiKey = Environment.GetEnvironmentVariable("OpenAIApiKey");
                client.DefaultRequestHeaders.Add(Common.Authorization, $"Bearer {apiKey}");
                var httpContent = new StringContent(JsonConvert.SerializeObject(_sessionMessage), Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync("https://api.openai.com/v1/chat/completions", httpContent);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    GptMetaResponse gptMetaResponse = JsonConvert.DeserializeObject<GptMetaResponse>(result);

                    // record GPT answer
                    GptMessage assistantMessage = new GptMessage()
                    {
                        Role = Role.assistant.ToString(),
                        Content = gptMetaResponse.Choices.First().Message.Content
                    };
                    _sessionMessage.Messages.Add(assistantMessage);
                }
            }
            UserMessageResponse userMessageResponse = new UserMessageResponse()
            {
                Message = _sessionMessage.Messages.Last().Content
            };
            return userMessageResponse;
        }
    }
}
