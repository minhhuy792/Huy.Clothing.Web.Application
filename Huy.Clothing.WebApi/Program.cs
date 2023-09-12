//using style net 6+
using Huy.Clothing.WebApi;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseWebRoot(Directory.GetCurrentDirectory())//wwwroot
        .UseStartup<Startup>();
    })
    .Build()
    .Run();
