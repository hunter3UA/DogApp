using System.Linq.Expressions;
using DogApp.Domain.DbEntities;
using DogApp.Domain.Enums;

namespace DogApp.Application.Repositories
{
    public interface IRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        Task<Guid> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetRangeAsync(
            Expression<Func<TEntity,object>>? sortingExpression,
            CancellationToken cancellationToken,
            int skip = default,
            int take = default,
            bool asNoTracking = true,
            SortingOrder sortingOrder = SortingOrder.Asc);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken CancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
