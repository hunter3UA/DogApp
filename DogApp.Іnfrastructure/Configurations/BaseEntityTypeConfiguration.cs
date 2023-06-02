using DogApp.Domain.DbEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DogApp.Іnfrastructure.Configurations
{
    public class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id).HasName("pk_dogs");
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }
}
