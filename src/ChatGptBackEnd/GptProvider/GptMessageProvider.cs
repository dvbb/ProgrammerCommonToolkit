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
        Task<GptMetaRequest> SendGptMessage(GptMetaRequest sessionMessage, string content);
    }
    public class GptMessageProvider : IGptMessageProvider
    {
        public readonly ICallGptRepository _callGptRepository;
        public GptMessageProvider(ICallGptRepository callGptRepository)
        {
            _callGptRepository = callGptRepository;
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

        public async Task<GptMetaRequest> SendGptMessage(GptMetaRequest sessionMessage, string content)
        {
            GptMessage userMessage = new GptMessage()
            {
                Role = Role.user.ToString(),
                Content = content
            };
            sessionMessage.Messages.Add(userMessage);

            // send a http request to GPT server
            using (var client = new HttpClient())
            {
                var apiKey = Environment.GetEnvironmentVariable("OpenAIApiKey");
                client.DefaultRequestHeaders.Add(Common.Authorization, $"Bearer {apiKey}");
                var httpContent = new StringContent(JsonConvert.SerializeObject(sessionMessage), Encoding.UTF8, "application/json");
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
                    sessionMessage.Messages.Add(assistantMessage);
                }
            }
            return sessionMessage;
        }
    }
}
