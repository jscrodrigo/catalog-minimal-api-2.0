using Catalog.Context;
using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Endpoints
{
    public static class ProductEndpointsExtensions
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapPost("/products/includeProduct", async (Product product, CatalogDbContext catalogDbContext) =>
            {
                catalogDbContext.Products.Add(product);
                await catalogDbContext.SaveChangesAsync();
                return Results.Ok(product);
            }).WithTags("Product");

            app.MapGet("/products/existingProducts", async (CatalogDbContext catalogDbContext) =>
            {

                var products = await catalogDbContext.Products.OrderByDescending(p => p.Id).ToListAsync();

                return (!products.Any()) ? Results.NotFound("No products registered") : Results.Ok(products);
            }).WithTags("Product");

            app.MapGet("/products/returnProduct/{id}", async (int id, CatalogDbContext catalogDbContext) =>
            {
                var product = await catalogDbContext.Products.FindAsync(id);

                return (null != product) ? Results.Ok(product) : Results.NotFound($"Product {id} not found");
            }).WithTags("Product");

            app.MapPut("/products/updatProduct/{id:int}", async (int id, Product product, CatalogDbContext catalogDbContext) =>
            {
                if (id != product.Id)
                {
                    return Results.BadRequest("The IDs must be the same");
                }

                var storedProduct = await catalogDbContext.Products.FindAsync(id);

                if (storedProduct is null)
                {
                    return Results.NotFound($"Product {id} not found!");
                }

                storedProduct.Name = product.Name;
                storedProduct.Description = product.Description;
                storedProduct.Category = product.Category;
                storedProduct.Amount = product.Amount;
                storedProduct.RegisterDate = product.RegisterDate;

                await catalogDbContext.SaveChangesAsync();
                return Results.Ok(storedProduct);
            }).WithTags("Product");

            app.MapDelete("/products/deleteProduct/{id:int}", async (int id, CatalogDbContext catalogDbContext) =>
            {
                var product = await catalogDbContext.Products.FindAsync(id);

                if (product is null)
                {
                    return Results.NotFound($"Category {id} not found!");
                }

                catalogDbContext.Products.Remove(product);
                await catalogDbContext.SaveChangesAsync();

                return Results.NoContent();
            }).WithTags("Product");
        }
    }
}
