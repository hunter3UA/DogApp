using System.Text.Json.Serialization;

namespace DogApp.Application.Dtos.Dog
{
    public record DogDto
    {
        public string? Name { get; set; }

        public string? Color { get; set; }

        [JsonPropertyName("tail_length")]
        public double TailLength { get; set; }

        public double Weight { get; set; }
    }
}
