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

    public async Task DeleteAsync(T entity, bool saveChanges = true)
    {
        try
        {
            Entities.Remove(entity);
            if (saveChanges)
            {
                await ApplicationDbContext.DbContext.SaveChangesAsync();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task DeleteAsync(int id, bool saveChanges = true)
    {
        var entity = await Entities.FindAsync(id);
        await DeleteAsync(entity);
        if(saveChanges)
        {
            await ApplicationDbContext.DbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        Entities.RemoveRange(entities);
        if (saveChanges)
        {
            await ApplicationDbContext.DbContext.SaveChangesAsync();
        }
    }

    public T Find(params object[] keyValues) => Entities.Find(keyValues);

    public async Task<T> FindAsync(params object[] keyValues) => await Entities.FindAsync(keyValues);

    public async Task<IList<T>> GetAllAsync() => await Entities.ToListAsync();

    public async Task InsertAsync(T entity, bool saveChanges = true)
    {
        await Entities.AddAsync(entity);
        if (saveChanges)
        {
           await ApplicationDbContext.DbContext.SaveChangesAsync();
        }
    }
        

    public async Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        await ApplicationDbContext.DbContext.AddRangeAsync(entities);
        if (saveChanges)
        {
           await ApplicationDbContext.DbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(T entity, bool saveChanges = true)
    {
        Entities.Update(entity);
        if (saveChanges)
        {
            await ApplicationDbContext.DbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
    {
        Entities.UpdateRange(entities);

    }
}
