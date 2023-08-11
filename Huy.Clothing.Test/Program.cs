using Huy.Clothing.Application.Interfaces.Common;
using Huy.Clothing.Infrastructrure.Context;
using Huy.Clothing.Shared;
namespace Huy.Clothing.Test;

internal class Program
{
    static void Main(string[] args)
    {
        /*
        SolidStoreContext db = new();
        var products = db.Products.ToList();
        foreach ( var product in products )
        {
            Console.WriteLine(product.ProductId + " " + product.ProductName);
        }
        */
        //anonymous methods
        DbFactoryContext dbContext = new DbFactoryContext(() => new SolidStoreContext());
        IApplicationDbContext db = new ApplicationDbContext(dbContext);
        Console.ReadLine();

    }
}