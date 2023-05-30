using Catalog.Context;
using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Endpoints
{
    public static class CategoryEndpointsExtensions
    {
        public static void MapCategoryEndpoints(this WebApplication app)
        {
            app.MapPost("/categories/createCategory", async (Category category, CatalogDbContext catalogDbContext) =>
            {
                catalogDbContext.Categories?.Add(category);
                await catalogDbContext.SaveChangesAsync();

                return Results.Created($"/categories/{category.Id}", category);
            }).WithName("CreateCategory")
              .WithTags("Category");

            app.MapGet("/categories/existingCategories", async (CatalogDbContext catalogDbContext) =>
            {
                var categories = await catalogDbContext.Categories.OrderByDescending(c => c.Id).ToListAsync();
                return (categories.Count > 0) ? Results.Ok(categories) : Results.NotFound("There's no categories registered");
            }).WithName("ReturnExistingCategories")
              .RequireAuthorization()
              .WithTags("Category");

            app.MapGet("/categories/returnCategory/{id}", async (int id, CatalogDbContext catalogDbContext) =>
            {
                var category = await catalogDbContext.Categories.FindAsync(id);

                return (null != category) ? Results.Ok(category) : Results.NotFound($"Category {id} not found");
            }).WithName("GetCategoryById")
              .RequireAuthorization()
              .WithTags("Category");

            app.MapPut("/categories/updateategory/{id:int}", async (int id, Category category,
                CatalogDbContext catalogDbContext) =>
            {
                if (id != category.Id)
                {
                    return Results.BadRequest("The IDs must be the same");
                }

                var storedCategory = await catalogDbContext.Categories.FindAsync(id);

                if (storedCategory is null)
                {
                    return Results.NotFound($"Category {id} not found!");
                }

                storedCategory.Name = category.Name;
                storedCategory.Description = category.Description;

                await catalogDbContext.SaveChangesAsync();
                return Results.Ok(storedCategory);
            }).WithName("UpdateCategoryById")
              .RequireAuthorization()
              .WithTags("Category");

            app.MapDelete("/categories/deleteCategory/{id:int}", async (int id, CatalogDbContext catalogDbContext) =>
            {
                var category = await catalogDbContext.Categories.FindAsync(id);

                if (category is null)
                {
                    return Results.NotFound($"Category {id} not found!");
                }

                catalogDbContext.Categories.Remove(category);
                await catalogDbContext.SaveChangesAsync();

                return Results.NoContent();
            }).WithName("DeleteCategoryByid")
              .RequireAuthorization()
              .WithTags("Category");
        }
    }
}
