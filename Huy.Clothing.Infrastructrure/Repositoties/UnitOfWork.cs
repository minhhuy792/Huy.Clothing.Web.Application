using Microsoft.EntityFrameworkCore;

namespace Huy.Clothing.Infrastructrure.Repositoties;

public class UnitOfWork : IUnitOfWork
{
    // caching repository after initialized turn (Redis)
    private Dictionary<string, object> Repositories {  get;}
    private IDbContextTransaction _transaction;
    private IsolationLevel? _isolationLevel;
    public IApplicationDbContext ApplicationDbContext { get; private set; }

    public UnitOfWork(IApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        this.Repositories = new Dictionary<string, dynamic>();
    }

    public async Task BeginTransactionAsync()
    {
        await StartNewTransactTionIfNeeded();
    }

    public async Task CommitTransactionAsync()
    {
        await ApplicationDbContext.DbContext.SaveChangesAsync();
        if (_transaction is null) return;
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync(); //giai phong transaction
        _transaction = null;
    }
    /// <summary>
    /// Return a instance of repository 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T); //lay kieu cua entity nao do 
        var typeName = type.Name;
        //lock: giup dong bo hoa cac loi goi tao Repository
        lock (Repositories)
        {
            if (Repositories.ContainsKey(typeName))
            {
                return (IRepository<T>)Repositories[typeName];
            }
            var repository = new Repository<T>(ApplicationDbContext);
            //add vao cache
            Repositories.Add(typeName, repository);
            //return
            return repository;

        }

    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync(); //giai phong transaction
        _transaction = null;
    }

    public async Task<int> saveChangesAsync(CancellationToken cancellationToken = default)
    => await ApplicationDbContext.DbContext.SaveChangesAsync(cancellationToken);
    /// <summary>
    /// Begin a transaction
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private async Task StartNewTransactTionIfNeeded()
    {
        if(_transaction == null)
        {
            _transaction = _isolationLevel.HasValue ?
                await ApplicationDbContext.DbContext.Database.BeginTransactionAsync(_isolationLevel.GetValueOrDefault()):
                await ApplicationDbContext.DbContext.Database.BeginTransactionAsync();
        }
    }

    public void Dispose()
    {
        //nothing: connection database, nothing object
        if(ApplicationDbContext == null) return;
        //close connect
        if(ApplicationDbContext.DbContext.Database.GetDbConnection().State == ConnectionState.Open)
        {
            ApplicationDbContext.DbContext.Database.GetDbConnection().Close();
        }
        ApplicationDbContext.DbContext.Dispose();
        ApplicationDbContext = null;
        GC.SuppressFinalize(this); //nothing all object (unitofwork)
    }
}
