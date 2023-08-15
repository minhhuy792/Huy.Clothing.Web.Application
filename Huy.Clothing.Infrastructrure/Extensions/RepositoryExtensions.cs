using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Huy.Clothing.Infrastructrure.Extensions;

public static class RepositoryExtensions
{
    /// <summary>
    /// Where method of IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="repository"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IQueryable<T> Where<T>(this IRepository<T> repository,
        Expression<Func<T, bool>> predicate) where T : class
    => repository.Entities.Where(predicate);

    /// <summary>
    /// Get listx T by entity 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="repository"></param>
    /// <returns></returns>
    public static async Task<List<T>> toListAsync<T>(this IRepository<T> repository) where T : class
        => await repository.toListAsync();

    public static async Task<List<T>> toListAsync<T>(this IRepository<T> repository,
        Expression<Func<T, bool>> predicate) where T : class
        => await repository.Where(predicate).ToListAsync();

    //Using linq :orderby
    public static IOrderedQueryable<T> OrderBy<T, TKey>(this IRepository<T> repository,
        Expression<Func<T, TKey>> keySelector) where T : class
    => repository.Entities.OrderBy(keySelector);
    //return single T
    public static async Task<T> FirstOrDefaultAsync<T>(this IRepository<T> repository,
        Expression<Func<T, bool>> predicate) where T : class
    => await repository.FirstOrDefaultAsync(predicate);

    //return class Entity
    public static async Task<T> ClassOrDefaultAsync<T>(this IRepository<T> repository,
        Expression<Func<T, bool>> predicate) where T : class
    => await repository.ClassOrDefaultAsync(predicate);
    //check exit a entity T
    public static async Task<bool> AnyAsync<T>(this IRepository<T> repository,
        Expression<Func<T,bool>> predicate) where T : class
     => await repository.Entities.AnyAsync(predicate);

    //count exist entity T 
    public static async Task<int> CountAsync<T>(this IRepository<T> repository,
        Expression<Func<T, bool>> predicate) where T : class
     => await repository.Entities.CountAsync(predicate);

    //Select
    public static IQueryable<TResult> Select<T,TResult>(this IRepository<T> repository,
        Expression<Func<T, TResult>> predicate) where T : class 
    => repository.Entities.Select(predicate);
}