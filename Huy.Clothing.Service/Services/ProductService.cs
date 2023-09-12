using Huy.Clothing.Caching;
using Huy.Clothing.Caching.Common;
using Huy.Clothing.Caching.Extensions;

namespace Huy.Clothing.Service.Services;

public class ProductService : DataServiceBase<Product>, IProductService
{
    private readonly IDataCached dataCached;
    public ProductService(IUnitOfWork unitOfWork, IDataCached dataCached) : base(unitOfWork)
    {
        this.dataCached = dataCached;
    }

    public override async Task AddAsync(Product entity)
    {
        string key = null;
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            //luu vao database
            await UnitOfWork.Repository<Product>().InsertAsync(entity);
            //luu vao cache
            key = string.Format(CachingCommonDefaults.CacheKey, typeof(Product).Name, entity.ProductId);
            dataCached.Set(key, entity,CachingCommonDefaults.CacheTime);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch(Exception ex)
        { 
            await UnitOfWork.RollbackTransactionAsync();
            dataCached.Remove(key);
            throw;
        }
    }

    public override async Task DeleteAsync(int? id)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            //1.get repository of Product
            var productRepo = UnitOfWork.Repository<Product>();
            //2.get product instance by id
            var product = await productRepo.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException();
            await productRepo.DeleteAsync(product);
            //remove product in cached
            string key = string.Format(CachingCommonDefaults.CacheKey,typeof(Product).Name, id);
            dataCached.Remove(key);
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
        }
    }

    public override async Task DeleteAsync(Product entity)
    => await UnitOfWork.Repository<Product>().DeleteAsync(entity);
    /*
    public override async Task<IList<Product>> GetAllAsync()
    => await UnitOfWork.Repository<Product>().GetAllAsync();
    */
    /*call via caching*/
    public override async Task<IList<Product>> GetAllAsync()
    {
        string name = typeof(Product).Name.ToLower();//"product"
        string keyAll = string.Format(CachingCommonDefaults.AllCacheKey, name);
        //1.check xem toan bo du lieu Products da duoc cached chua?
        if (!dataCached.IsSet(keyAll) || !(bool)dataCached.Get(keyAll))
        {
            /* load all product tu db => cached.Sau do set value true cho keyall*/
            await _loadAllProductToCached();
            dataCached.Set(keyAll, true,CachingCommonDefaults.CacheTime);
        }
        //2.get all data tu cached ra ngoai
        string pattern = string.Format(CachingCommonDefaults.CacheKeyHeader,name);//huy.clothing.product
        var result = dataCached.GetValues<Product>(pattern);
        return await Task.FromResult(result);
    }

    private async Task _loadAllProductToCached()
    {
        /*goi tu db len*/
        var products = await UnitOfWork.Repository<Product>().GetAllAsync();
        string key = string.Empty;
        foreach (var product in products)
        {
            /*lam sao sinh chuoi ky tu doi tuong p o tren? : huy.clothing.product.10*/
            key = dataCached.GetKey(product,product =>product.ProductId);
            /*check xem key da duoc cached chua? neu chua thi set vao cached*/
            if(!dataCached.IsSet(key))
                dataCached.Set(key, product,CachingCommonDefaults.CacheTime);
        }

    }

    public override async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    => await UnitOfWork.Repository<Product>().Where(predicate).ToListAsync();

    public override async Task<Product> GetOneAsync(int? id)
    {
        /*check trong cached neu co => lay tu cache, ko co thi load tu db*/
        try
        {
            string key = string.Format(CachingCommonDefaults.CacheKey,typeof(Product).Name.ToLower(),id);
            if (dataCached.IsSet(key))
                return await Task.FromResult(dataCached.Get<Product>(key));
            Product? p = await UnitOfWork.Repository<Product>().FindAsync(id);
            if(p != null)
            {
                dataCached.Set(key, p, CachingCommonDefaults.CacheTime);
                return await Task.FromResult(p);
            }
            return null!;
                
        }
        catch (Exception ex)
        {
            return null!;
        }
    }

    public override async Task UpdateAsync(Product entity)
    {
        try
        {
            await UnitOfWork.BeginTransactionAsync();
            //1.get repository of Product
            var productRepo = UnitOfWork.Repository<Product>();
            //2.get product instance by id
            var product = await productRepo.FindAsync(entity.ProductId);
            if (product == null)
                throw new KeyNotFoundException();
            //category = entity;
            product.ProductName = entity.ProductName;
            await UnitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await UnitOfWork.RollbackTransactionAsync();
        }
    }
}
