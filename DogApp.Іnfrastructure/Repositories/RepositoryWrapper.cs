using DogApp.Application.Repositories;
using DogApp.Іnfrastructure.DbContexts;

namespace DogApp.Іnfrastructure.Repositories
{
    public sealed class RepositoryWrapper : IRepositoryWrapper
    {
        private DogDbContext _dbContext;

        public IDogRepository Dogs { get; }

        public RepositoryWrapper(DogDbContext dbContext, IDogRepository dogs)
        {
            _dbContext = dbContext;
            Dogs = dogs;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}