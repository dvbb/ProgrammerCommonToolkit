using ChatGptBackEnd.DataTransferModel;
using ChatGptBackEnd.GptProvider;
using ChatGptBackEnd.MetaModel;
using Newtonsoft.Json;
using ProgrammerToolkit.Backend.IProvider;
using ProgrammerToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerToolkit.Test
{
    internal class GptTests
    {
        private IGptMessageProvider _provider;
        public GptTests()
        {
        }
        public GptTests(IGptMessageProvider iGptMessageProvider)
        {
            _provider = iGptMessageProvider;
        }

        [Test]
        public async Task ChatTest()
        {
            UserMessageRequest request = new UserMessageRequest
            {
                Message = "Hello GPT",
                Role = Role.user.ToString()
            };
            var response = await _provider.SendGptMessage(request);
            Console.WriteLine(response.Message);
        }

        [Test]
        public async Task SameSessionTest()
        {
            GptMetaRequest request = new GptMetaRequest();
            request.Model = Common.Gpt35Model;
            request.Messages = new List<GptMessage>()
            {
                new GptMessage()
                {
                    Content = "这是第一句话",
                    Role = Role.user.ToString(),
                }
            };

            var jsonReq = JsonConvert.SerializeObject(request);
            Uri uri = new Uri("https://api.openai.com/v1/chat/completions");
            var apiKey = Environment.GetEnvironmentVariable("OpenAIApiKey");

            //1
            var client = new HttpClient();
            var content = new StringContent(jsonReq, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add(Common.Authorization, $"Bearer {apiKey}");
            var httpResponse = await client.PostAsync(uri, content);
            IEnumerable<string> cookies = httpResponse.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            var result = await httpResponse.Content.ReadAsStringAsync();
            GptMetaResponse response = JsonConvert.DeserializeObject<GptMetaResponse>(result);
            Console.WriteLine(response.Choices[0].Message.Content);

            //2
            request.Messages[0].Content = "这是一个新的会话吗？你能查看我之前说的一句话吗";
            jsonReq = JsonConvert.SerializeObject(request);
            content = new StringContent(jsonReq, Encoding.UTF8, "application/json");
            httpResponse = await client.PostAsync(uri, content);
            cookies = httpResponse.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            result = await httpResponse.Content.ReadAsStringAsync();
            response = JsonConvert.DeserializeObject<GptMetaResponse>(result);
            Console.WriteLine(response.Choices[0].Message.Content);
        }
    }
}
