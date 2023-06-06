using Newtonsoft.Json;

namespace DogApp.Application.Models
{
    public class ErrorModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Key { get; set; }

        public required string Message { get; set; }
    }
}
