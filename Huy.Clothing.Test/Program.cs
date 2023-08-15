using Huy.Clothing.Application.Interfaces.Common;
using Huy.Clothing.Application.Interfaces.Repositories;
using Huy.Clothing.Infrastructrure.Context;
using Huy.Clothing.Infrastructrure.Extensions;
using Huy.Clothing.Infrastructrure.Repositoties;
using Huy.Clothing.Shared;
using Microsoft.EntityFrameworkCore;

namespace Huy.Clothing.Test;

internal class Program
{
    static void Main(string[] args)
    {
        //1.lay ra mot mang cac products => test xem repository chay the nao ?
        DbFactoryContext dbContext = new DbFactoryContext(() => new SolidStoreContext());
        //2.init ApplicationDbContext
        IApplicationDbContext db = new ApplicationDbContext(dbContext);
        //3.Khoi tao repository
        IRepository<Product> productRepository = new Repository<Product>(db);
        /*
        var data = productRepository.Find(1);
        Console.WriteLine($"Product ID: {data.ProductId}, Product Name: {data.ProductName}");
        */
        var products = productRepository.Select(x => x.ProductName).ToList();
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
        Console.ReadLine();

    }
}