using Huy.Clothing.Shared;
using Microsoft.EntityFrameworkCore;

namespace Huy.Clothing.Infrastructrure.Context;
/// <summary>
/// implement design pattern faction to create a object related database operations
/// </summary>
public class DbFactoryContext
{
    //fields
    private DbContext _dbContext;
    private Func<SolidStoreContext> _instanceFunc;

    //properties
    public DbContext DbContext => this._dbContext ?? (this._dbContext = _instanceFunc.Invoke());

    public DbFactoryContext(Func<SolidStoreContext> dbContextFactory)
    {
        _instanceFunc = dbContextFactory;
    }
}
