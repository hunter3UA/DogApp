using System.Text.Json.Serialization;

namespace DogApp.Application.Dtos.Dog
{
    public sealed record DogDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Color { get; set; }

        [JsonPropertyName("tail_length")]
        public double TailLength { get; set; }

        public double Weight { get; set; }
    }
}
