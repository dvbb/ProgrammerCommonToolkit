using ChatGptBackEnd.MetaModel;
using Newtonsoft.Json;
using ProgrammerToolkit.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBackEnd.GptRepository
{
    public interface ICallGptRepository
    {
        Task<GptMetaResponse> MakeContent(GptMetaRequest request); 
    }
    public class CallGptRepository : ICallGptRepository
    {
        public async Task<GptMetaResponse> MakeContent(GptMetaRequest request)
        {
            try
            {
                var jsonReq = JsonConvert.SerializeObject(request);
                var url = "https://api.openai.com/v1/chat/completions";
                var apiKey = Environment.GetEnvironmentVariable("OpenAIApiKey");
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonReq, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add(Common.Authorization, $"Bearer {apiKey}");
                    var httpResponse = await client.PostAsync(url, content);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = await httpResponse.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<GptMetaResponse>(result);
                        return response;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
