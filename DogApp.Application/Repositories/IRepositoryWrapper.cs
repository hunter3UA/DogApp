using DogApp.Domain.DbEntities;

namespace DogApp.Application.Repositories
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<DbDog> Dogs { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
