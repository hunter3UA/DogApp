﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DogApp.Domain.DbEntities;
using DogApp.Application.Repositories;
using DogApp.Domain.Enums;
using DogApp.Іnfrastructure.Extensions;

namespace DogApp.Іnfrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : BaseEntity
    {
        private readonly DbContext _context; 

        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<Guid> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken)
        {
            var entry = await _context.AddAsync(entity, cancellationToken);

            return entry.Entity.Id;
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken cancellationToken)
        {
            var any = await _dbSet.AnyAsync(expression, cancellationToken);

            return any;
        }

        public async Task<IReadOnlyList<TEntity>> GetRangeAsync(
            CancellationToken cancellationToken,
            Expression<Func<TEntity, object>>? sortingExpression,
            SortingOrder sortingOrder = SortingOrder.Asc,
            int skip = default,
            int take = default,
            bool asNoTracking = true)
        {
            var sortedList = await _dbSet
                .NoTracking(asNoTracking)
                .GetFilteredQueryWithSorting(sortingExpression, sortingOrder, skip, take)
                .ToListAsync(cancellationToken);

            return sortedList;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            int count = await _dbSet.CountAsync(cancellationToken);

            return count;
        }
    }
}