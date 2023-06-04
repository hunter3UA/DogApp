using Newtonsoft.Json;

namespace DogApp.Api.Models
{
    public class ErrorModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? PropertyName { get; set; }

        public string Message { get; set; }
    }
}
