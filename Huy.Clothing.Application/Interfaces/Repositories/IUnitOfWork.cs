namespace Huy.Clothing.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Application db context
    /// </summary>
    IApplicationDbContext ApplicationDbContext { get; }
    /// <summary>
    /// Get repository instance of an entity (T) inside Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>

    IRepository<T> Repository<T>() where T : class;
    Task <int> saveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
