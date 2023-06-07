using DogApp.Domain.DbEntities;

namespace DogApp.Application.Repositories
{
    public interface IDogRepository : IRepositoryBase<DbDog>
    {
    }
}