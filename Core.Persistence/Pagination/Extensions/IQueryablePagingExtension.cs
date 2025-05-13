using Core.Persistence.Pagination;
using Core.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Pagination.Extensions
{
    public static class IQueryablePagingExtension
    {
        public static async Task<Paging<TEntity>> ToPaging<TEntity>(
            this IQueryable<TEntity> values,
            int index,
            int size,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, new()
        {

            Paging<TEntity> paging = new Paging<TEntity>();

            paging.Count = await values.CountAsync(cancellationToken);
            paging.Items = await values
                .Skip(index * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            paging.Index = index;
            paging.Size = size;
            paging.Pages = (int)Math.Ceiling(paging.Count / (double)size);

            return paging;
        }
    }
}
