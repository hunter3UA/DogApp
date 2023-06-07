using DogApp.Application.Repositories;
using DogApp.Domain.DbEntities;
using DogApp.Іnfrastructure.DbContexts;

namespace DogApp.Іnfrastructure.Repositories
{
    public class DogRepository : RepositoryBase<DbDog>, IDogRepository
    {
        public DogRepository(DogDbContext context) : base(context)
        {
        }
    }
}