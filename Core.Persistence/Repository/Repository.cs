using Core.Persistence.Dynamic;
using Core.Persistence.Pagination;
using Core.Persistence.Pagination.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {

        protected readonly DbContext Context;

        public Repository(DbContext _context)
        {
            Context = _context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {

            entities.ToList().ForEach(entity =>
            {
                entity.CreatedAt = DateTime.UtcNow;
            });

            await Context.Set<TEntity>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (predicate is not null)
                query = query.Where(predicate);

            if (withDeleted)
                query = query.IgnoreQueryFilters();

            if (enableTracking)
                query = query.AsTracking();
            else
                query = query.AsNoTracking();

            return query.AnyAsync(cancellationToken);

            throw new NotImplementedException();
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities)
        {
            entities.ToList().ForEach(entity =>
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
            });

            Context.Set<TEntity>().RemoveRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (enableTracking)
                query = query.AsTracking();
            else
                query = query.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            if (withDeleted)
                query = query.IgnoreQueryFilters();



            if (include is not null)
                query = include(query);

            return query.FirstOrDefaultAsync(cancellationToken);

        }

        public Task<Paging<TEntity>> GetListPagedAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (enableTracking)
                query = query.AsTracking();
            else
                query = query.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                query = orderBy(query);

            if (withDeleted)
                query = query.IgnoreQueryFilters();

            if (include is not null)
                query = include(query);

            return query.ToPagingAsync(index, size, cancellationToken);
        }

        public async Task<Paging<TEntity>> GetListByDynamic(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().ToDynamic(dynamic);

            if (!enableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (withDeleted)
                query = query.IgnoreQueryFilters();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToPagingAsync(index, size, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
        {
            entities.ToList().ForEach(entity =>
            {
                entity.UpdatedAt = DateTime.UtcNow;
            });

            Context.Set<TEntity>().UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;

        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (enableTracking)
                query = query.AsTracking();
            else
                query = query.AsNoTracking();

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                query = orderBy(query);

            if (withDeleted)
                query = query.IgnoreQueryFilters();

            if (include is not null)
                query = include(query);

            return query.ToListAsync();
        }
    }
}
