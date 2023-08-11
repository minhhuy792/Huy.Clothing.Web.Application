using Huy.Clothing.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huy.Clothing.Infrastructrure.Repositoties;
public class Repository<T> : IRepository<T> where T : class
{
    public IApplicationDbContext ApplicationDbContext { get; private set; }

    public Repository(IApplicationDbContext applicationDbContext) => ApplicationDbContext = applicationDbContext;

    public DbSet<T> Entities => ApplicationDbContext.DbContext.Set<T>();

    public Task DeleteAsync(T entity, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }

    public T Find(params object[] keyValues) => Entities.Find(keyValues);

    public async Task<T> FindAsync(params object[] keyValues) => await Entities.FindAsync(keyValues);

    public async Task<IList<T>> GetAllAsync() => await Entities.ToListAsync();

    public Task InsertAsync(T entity, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }

    public Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        throw new NotImplementedException();
    }
}
