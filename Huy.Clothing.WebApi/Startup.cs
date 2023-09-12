using Huy.Clothing.Caching.Extensions;
using Huy.Clothing.Service.Extensions;
using Huy.Clothing.WebApi.Security;

namespace Huy.Clothing.WebApi;

public class Startup
{
    public IConfiguration Configuration { get;}

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMemoryCache();

        //goi service
        services.ClothingInfrastructureDatabase(Configuration);
        services.AddDataServices();
        services.AddCacheServices();

        //security
        services.AddCors();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if(env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Huy WebApi v1"));
        }
        else
        {
            app.UseHsts();
        }
        app.UseMiddleware<SecurityHeadersMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseCors(configurePolicy:options =>
        {
            options.WithMethods("GET", "POST", "PUT", "DELETE");
            options.WithOrigins("https://abc.com");
        }
            );
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
