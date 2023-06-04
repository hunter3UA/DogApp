using Newtonsoft.Json;

namespace DogApp.Api.Models
{
    public sealed class ErrorModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? PropertyName { get; set; }

        public string Message { get; set; }
    }
}
