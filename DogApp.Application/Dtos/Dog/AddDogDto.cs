using System.Text.Json.Serialization;

namespace DogApp.Application.Dtos.Dog
{
    public record AddDogDto(
        string Name,
        string Color,
        [property: JsonPropertyName("tail_length")] double TailLength,
        double Weight);
}
