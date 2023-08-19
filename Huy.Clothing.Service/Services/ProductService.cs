namespace Huy.Clothing.Service.Services;

public class ProductService : DataServiceBase<Product>, IProductService
{
    public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task AddAsync(Product entity)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            await UnitOfWork.Repository<Product>().InsertAsync(entity);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch(Exception ex)
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
            //1.get repository of Product
            var productRepo = UnitOfWork.Repository<Category>();
            //2.get product instance by id
            var product = await productRepo.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException();
            await productRepo.DeleteAsync(product);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
        }
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
