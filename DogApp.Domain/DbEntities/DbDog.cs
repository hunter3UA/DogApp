namespace DogApp.Domain.DbEntities
{
    public class DbDog : BaseEntity
    {
        public required string Name { get; set; }

        public required string Color { get; set; }

        public double TailLength { get; set; }

        public double Weight { get; set; }
    }
}
