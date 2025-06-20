﻿using Core.Persistence.Dynamic;
using Core.Persistence.Pagination;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
            );

        Task<Paging<TEntity>> GetListPagedAsync(
                Expression<Func<TEntity, bool>>? predicate = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                int index = 0,
                int size = 10,
                bool withDeleted = false,
                bool enableTracking = true,
                CancellationToken cancellationToken = default
            );

        Task<List<TEntity>> GetListAsync(
               Expression<Func<TEntity, bool>>? predicate = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
               bool withDeleted = false,
               bool enableTracking = true,
               CancellationToken cancellationToken = default
           );

        Task<Paging<TEntity>> GetListByDynamic(
                DynamicQuery dynamic,
                Expression<Func<TEntity, bool>>? predicate = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                int index = 0,
                int size = 10,
                bool withDeleted = false,
                bool enableTracking = true,
                CancellationToken cancellationToken = default
        );

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
            );

        Task<TEntity> AddAsync(TEntity entity);

        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities);

    }
}
