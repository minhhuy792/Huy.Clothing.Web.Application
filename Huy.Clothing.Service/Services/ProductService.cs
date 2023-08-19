using Huy.Clothing.Application.Interfaces.Repositories;
using Huy.Clothing.Application.Interfaces.Services;
using Huy.Clothing.Infrastructrure.Extensions;
using Huy.Clothing.Service.BaseServices;
using Huy.Clothing.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Huy.Clothing.Service.Services;

public class ProductService : DataServiceBase<Product>, IProductService
{
    public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override Task AddAsync(Product entity)
    => UnitOfWork.Repository<Product>().InsertAsync(entity);

    public override async Task DeleteAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public override async Task DeleteAsync(Product entity)
    => await UnitOfWork.Repository<Product>().DeleteAsync(entity);

    public override async Task<IList<Product>> GetAllAsync()
    => await UnitOfWork.Repository<Product>().GetAllAsync();

    public override async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    => await UnitOfWork.Repository<Product>().Where(predicate).ToListAsync();

    public override Task<Product> GetOneAsync(int? id)
    => UnitOfWork.Repository<Product>().FindAsync(id);

    public override async Task UpdateAsync(Product entity)
    => await UnitOfWork.Repository<Product>().UpdateAsync(entity);
}
