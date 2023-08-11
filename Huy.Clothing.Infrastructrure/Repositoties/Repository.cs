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
    public IApplicationDbContext ApplicationDbContext => throw new NotImplementedException();
    public DbSet<T> Entities => throw new NotImplementedException();

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

    public T Find(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public Task<T> FindAsync(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public Task<IList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

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
