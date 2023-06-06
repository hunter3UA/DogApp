namespace DogApp.Domain.Entities
{
    public sealed class DogEntity
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Color { get; set; }

        public double TailLength { get; set; }

        public double Weight { get; set; }
    }
}
