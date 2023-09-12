namespace Huy.Clothing.Service.Services;

public class CategoryService : DataServiceBase<Category>, ICategoryService
{
    public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task AddAsync(Category entity)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            await UnitOfWork.Repository<Category>().InsertAsync(entity);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public override async Task DeleteAsync(int? id)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            //1.get repository of Category
            var categoryRepo = UnitOfWork.Repository<Category>();
            //2.get category instance by id
            var category = await categoryRepo.FindAsync(id);
            if (category == null) 
                throw new KeyNotFoundException();
            await categoryRepo.DeleteAsync(category);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
        }
    }

    public override async Task DeleteAsync(Category entity)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            await UnitOfWork.Repository<Category>().DeleteAsync(entity);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
        }
    }

    public override async Task<IList<Category>> GetAllAsync()
    => await UnitOfWork.Repository<Category>().Entities.ToListAsync();

    public override async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate)
    => await UnitOfWork.Repository<Category>().Entities.Where(predicate).ToListAsync();

    public override async Task<Category> GetOneAsync(int? id)
    => await UnitOfWork.Repository<Category>().FindAsync(id);

    public override async Task UpdateAsync(Category entity)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            //1.get repository of Category
            var categoryRepo = UnitOfWork.Repository<Category>();
            //2.get category instance by id
            var category = await categoryRepo.FindAsync(entity.CategoryId);
            if (category == null)
                throw new KeyNotFoundException();
            //category = entity;
            category.CategoryName = entity.CategoryName;
            category.Description = entity.Description;
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
        }
    }
}
