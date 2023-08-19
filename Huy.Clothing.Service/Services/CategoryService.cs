
using Huy.Clothing.Application.Interfaces.Repositories;
using Huy.Clothing.Application.Interfaces.Services;
using Huy.Clothing.Service.BaseServices;
using Huy.Clothing.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Huy.Clothing.Service.Services;

public class CategoryService : DataServiceBase<Category>, IProductCategoryService
{
    public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task AddAsync(Category entity)
    => await UnitOfWork.Repository<Category>().InsertAsync(entity);

    public override async Task DeleteAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public override Task DeleteAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public override async Task<IList<Category>> GetAllAsync()
    => await UnitOfWork.Repository<Category>().Entities.ToListAsync();

    public override async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate)
    => await UnitOfWork.Repository<Category>().Entities.Where(predicate).ToListAsync();

    public override async Task<Category> GetOneAsync(int? id)
    => await UnitOfWork.Repository<Category>().FindAsync(id);

    public override Task UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }
}
