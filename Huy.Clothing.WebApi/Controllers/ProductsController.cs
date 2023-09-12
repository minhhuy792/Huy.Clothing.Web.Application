using Microsoft.AspNetCore.Mvc;
using Huy.Clothing.Shared;
using Microsoft.AspNetCore.Authorization;
using System.Numerics;
using Huy.Clothing.Application.Interfaces.Services;
using Huy.Clothing.Caching;
using Huy.Clothing.Caching.Common;

namespace Huy.Clothing.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController:ControllerBase
{
    private readonly IProductService _productService;
    private readonly IDataCached _dataCached;

    public ProductsController(IProductService productService,IDataCached dataCached)
    {
        _productService = productService;
        _dataCached = dataCached;
    }

    //1.method get return a list products
    //GET:api/products
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Product>))]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        
        return await _productService.GetAllAsync();
    }

    //2.Get one by id
    //GET:api/products/[id]
    [HttpGet("{id}",Name = nameof(GetProduct))]//name for route
    [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetProduct(int? id)
    {
        Product? product = await _productService.GetOneAsync(id);
        if(product == null)
        {
            return NotFound();//return 404
        }
        return Ok(product);
    }

    //POST:api/products/(json/xml)
    //3. using post method
    [HttpPost]
    [ProducesResponseType(201,Type = typeof(Product))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody]Product product)
    {
        //logic
        try
        {
            if(product == null)
            {
                return BadRequest();//400
            }
            await _productService.AddAsync(product);//bien ham AddAsync co return id cua product da insert
            //return Ok();
            return CreatedAtRoute(
                routeName: nameof(GetProduct),
                routeValues: new {id = product.ProductId},
                value: product
                );
        }
        catch (Exception ex)
        {
            return BadRequest($"Repository fail to create the product {product.ProductId}");
        }
    }

    //4.Update
    //PUT (update):api/products/[id]
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int? id, [FromBody] Product product)
    {
        if (product == null || product.ProductId != id)
        {
            return BadRequest();//400 BadRequest
        }
        Product? existing = await _productService.GetOneAsync(id);
        if (existing == null)
            return NotFound();//404 Not Found
        //execute
        await _productService.UpdateAsync(product);
        return new NoContentResult();//204

    }

    //5.Delete
    //DELETE: api/products/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            Product? existing = await _productService.GetOneAsync(id);
            if (existing == null)
            {
                ProblemDetails problemDetails = new()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://mydomain/api/products/failed-to-delete",
                    Title = $"Product ID {id} found but failed to delete.",
                    Detail = "Detail for ...",
                    Instance = HttpContext.Request.Path
                };
                return BadRequest(problemDetails);
            }
            //if found 
            await _productService.DeleteAsync(id);
            return new NoContentResult();//204 no content
        }
        catch (Exception ex)
        {
            return BadRequest($"Product ID {id} was not found but failed to delete");
        }
    }
}
