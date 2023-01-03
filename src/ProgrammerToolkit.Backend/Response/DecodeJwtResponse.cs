using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ProgrammerToolkitBackend.Response
{
    public class DecodeJwtResponse: BaseResponse
    {
        [JsonProperty("decodedtoken")]
        public string DecodedToken { get; set; }
    }
}
