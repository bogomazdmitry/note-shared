using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NoteShared.Domain.Core
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task RemoveAsync(TEntity entity);

        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> GetAllQueryable();

        IQueryable<TEntity> GetAllByQueryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> include = null);
    }
}
