using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatGptBackEnd.MetaModel
{
    public class GptMetaResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("usage")]
        public GptUsage Usage { get; set; }

        [JsonProperty("choices")]
        public List<GptChoices> Choices { get; set; }

    }
    public class GptUsage
    {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("completion_tokens")]
        public string CompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        public string TotalTokens { get; set; }
    }
    public class GptChoices
    {
        [JsonProperty("message")]
        public GptMessage Message { get; set; }

        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }
    }
}
