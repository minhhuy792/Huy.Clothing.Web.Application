namespace Huy.Clothing.Service.Extensions;

public static class ClothingContextServiceExtensions
{
    //1.Tao ra 1 dich vu de thuc hien ket noi database nao do
    public static IServiceCollection ClothingInfrastructureDatabase(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<SolidStoreContext>(options =>
        {
        options.UseSqlServer(config.GetConnectionString("SolidStoreConn"),
            sqlOption => sqlOption.CommandTimeout(60));
        //options.UseLazyLoadingProxies();//must be install the package Microsoft.EntityFrameworkCore.Proxies
        });
        //add dbcontext by service
        services.AddScoped <Func<SolidStoreContext>>(
            provider => () => provider.GetService<SolidStoreContext>()
            );
        services.AddScoped<DbFactoryContext> ();//giong nhu new DbFactory
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    //2.data services
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        //services.AddScoped<ICategoryService,ICategoryService>();
        services.AddScoped<IProductService, ProductService>();
        // co IService khac ma muon add-on thi add vao day
        return services;
    }
    
}
