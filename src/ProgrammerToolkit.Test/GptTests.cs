using ChatGptBackEnd.DataTransferModel;
using ChatGptBackEnd.GptProvider;
using ChatGptBackEnd.MetaModel;
using Newtonsoft.Json;
using ProgrammerToolkit.Backend.IProvider;
using ProgrammerToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Ignore("TODO: Need to investigate how to use DI in test project")]
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
                    Content = "你好，GPT，之后的对话你可以在每句话结尾加一个S吗",
                    Role = Role.user.ToString(),
                }
            };

            var jsonReq = JsonConvert.SerializeObject(request);
            Uri uri = new Uri("https://api.openai.com/v1/chat/completions");
            var apiKey = Environment.GetEnvironmentVariable("OpenAIApiKey");

            //1
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            var client = new HttpClient(handler);
            var content = new StringContent(jsonReq, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add(Common.Authorization, $"Bearer {apiKey}");
            var httpResponse = await client.PostAsync(uri, content);
            IEnumerable<string> cookies = httpResponse.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            var result = await httpResponse.Content.ReadAsStringAsync();
            dynamic dynamicResult = JsonConvert.DeserializeObject(result);
            string sessionId = dynamicResult.id;
            GptMetaResponse response = JsonConvert.DeserializeObject<GptMetaResponse>(result);
            Console.WriteLine(response.Choices[0].Message.Content);
            Console.WriteLine(sessionId);
            Console.WriteLine("----------------------");

            //2
            request.Messages.Add(new GptMessage()
            {
                Role = Role.user.ToString(),
                Content = "Hi,GPT,可以给我讲一个笑话吗"
            });
            //request.Messages[0].Content = "我正在使用c#开发，HttpClient中如何加入session id";
            //request.Messages[0].Content = "我正在使用c#开发，如何获取GPT服务器返回的session id";
            jsonReq = JsonConvert.SerializeObject(request);
            content = new StringContent(jsonReq, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Cookie", "sessionId=" + sessionId);
            httpResponse = await client.PostAsync(uri, content);
            cookies = httpResponse.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            result = await httpResponse.Content.ReadAsStringAsync();
            dynamicResult = JsonConvert.DeserializeObject(result);
            sessionId = dynamicResult.id;
            response = JsonConvert.DeserializeObject<GptMetaResponse>(result);
            Console.WriteLine(response.Choices[0].Message.Content);
            Console.WriteLine(sessionId);
        }
    }
}
