using Newtonsoft.Json;

namespace DogApp.Application.Models
{
    public sealed class ErrorResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? PropertyName { get; set; }

        public string Message { get; set; }
    }
}
