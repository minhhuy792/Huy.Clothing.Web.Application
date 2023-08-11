namespace Huy.Clothing.Application.Base;

public interface IBaseReaderRepository<T> : IBaseRepo<T> where T : class
{
    /// <summary>
    /// Get all items of an entity by asynchronous (bat dong bo)
    /// </summary>
    /// <returns>List of T</returns>
    Task<IList<T>> GetAllAsync();

    T Find(params object[] keyValues);
    // T Find(object key);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    Task<T> FindAsync(params object[] keyValues);
}
