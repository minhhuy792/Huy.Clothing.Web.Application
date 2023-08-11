using Microsoft.EntityFrameworkCore;

namespace Huy.Clothing.Application.Base;

public interface IBaseRepo<T> where T : class
{
    //properties
    DbSet<T> Entities { get;} //giup ta lay mot collections cac entity T
    IApplicationDbContext ApplicationDbContext { get; }
}
