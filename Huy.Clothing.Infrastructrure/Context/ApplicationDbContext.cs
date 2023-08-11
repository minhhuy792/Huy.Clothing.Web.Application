using Microsoft.EntityFrameworkCore;

namespace Huy.Clothing.Infrastructrure.Context;

public class ApplicationDbContext : IApplicationDbContext
{
    private DbFactoryContext _dbFactoryContext;

    public ApplicationDbContext(DbFactoryContext dbFactoryContext)
    {
        _dbFactoryContext = dbFactoryContext;
    }

    public DbContext DbContext => this._dbFactoryContext.DbContext;
}
