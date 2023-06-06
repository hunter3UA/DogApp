using System.Linq.Expressions;
using DogApp.Domain.DbEntities;
using DogApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DogApp.Іnfrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> GetFilteredQueryWithSorting<TEntity>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, object>>? sortingExpression,
            SortingOrder sortingOrder,
            int skip = default,
            int take = default)
            where TEntity : BaseEntity
        {
            query = query.ApplySorting(sortingExpression, sortingOrder);

            if (skip != default)
                query = query.Skip(skip);

            if (take != default)
                query = query.Take(take);

            return query;
        }

        public static IQueryable<TEntity> NoTracking<TEntity>(
            this IQueryable<TEntity> query,
            bool asNoTracking)
            where TEntity : BaseEntity
        {
            return asNoTracking ? query.AsNoTracking() : query;
        }

        public static IQueryable<TEntity> ApplySorting<TEntity>(
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, object>>? sortingExpression,
            SortingOrder sortingOrder)
            where TEntity : BaseEntity
        {
            if (sortingExpression is null)
                return query;

            query = sortingOrder is SortingOrder.Asc ?
                query.OrderBy(sortingExpression) :
                query.OrderByDescending(sortingExpression);

            return query;
        }

    }
}