using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Huy.Clothing.Application.Interfaces.Services;

public interface IDataService<TEntity> where TEntity : class, new()
{
    Task<IList<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> predicate);
    Task<TEntity> GetOneAsync(int? id);
    Task UpdateAsync(TEntity entity);
    Task AddAsync(TEntity entity);
    Task DeleteAsync(int? id);
    Task DeleteAsync(TEntity entity);
}
