using DogApp.Domain.Constants;
using DogApp.Domain.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogApp.Іnfrastructure.Configurations
{
    public class DogEntityTypeConfiguration : BaseEntityTypeConfiguration<DbDog>
    {
        public override void Configure(EntityTypeBuilder<DbDog> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(EntityConstants.DogConstants.MaxDogNameLength)
                .IsRequired();

            builder.Property(p => p.Color)
                .HasColumnName("color")
                .HasMaxLength(EntityConstants.DogConstants.MaxDogColorLength)
                .IsRequired();

            builder.Property(p => p.TailLength).HasColumnName("tail_length").IsRequired();
            builder.Property(p => p.Weight).HasColumnName("weight").IsRequired();
            builder.HasIndex(p => p.Name).HasDatabaseName("ix_dogs_name").IsUnique();
            builder.ToTable("dogs", t =>
            {
                t.HasCheckConstraint("ck_tail_length", $"tail_length>=0 and tail_length<={EntityConstants.DogConstants.MaxDogTailLength}");
                t.HasCheckConstraint("ck_weight", $"weight>0 and weight<={EntityConstants.DogConstants.MaxDogWeight}");
            });

            builder.HasData(
                CreateDog("Jeck", "white", 10, 20.5),
                CreateDog("Alice", "black", 6, 15),
                CreateDog("Richard", "black & brown", 20, 40.7),
                CreateDog("Bob", "brown & white", 15, 30.3),
                CreateDog("Norman", "grey", 17, 55),
                CreateDog("David", "black", 35, 40),
                CreateDog("Neo", "brown", 20, 44.7),
                CreateDog("Jessy", "brown & white", 23.3, 37.6));
        }

        private DbDog CreateDog(string name, string color, double tailLength, double weight)
        {
            return new DbDog
            {
                Id = Guid.NewGuid(),
                Name = name,
                Color = color,
                TailLength = tailLength,
                Weight = weight
            };
        }
    }
}
