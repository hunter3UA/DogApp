using Newtonsoft.Json;

namespace DogApp.Application.Models
{
    public sealed class ErrorResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Key { get; set; }

        public required string Message { get; set; }
    }
}
