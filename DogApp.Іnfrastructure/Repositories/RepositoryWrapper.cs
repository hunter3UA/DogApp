using DogApp.Application.Repositories;
using DogApp.Domain.DbEntities;
using DogApp.Іnfrastructure.DbContexts;

namespace DogApp.Іnfrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DogDbContext _dbContext;

        public IRepositoryBase<DbDog> Dogs { get; }

        public RepositoryWrapper(DogDbContext dbContext,
            IRepositoryBase<DbDog> _dogs)
        {
            _dbContext = dbContext;
            Dogs = _dogs;
        }
        
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
