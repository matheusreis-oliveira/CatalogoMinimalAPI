using CatalogoMinimal.Data.Context;
using CatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMinimal.Endpoints;

public static class WebApplicationEndpoint
{
    public static void MapEntityEndpoints<T, TId>(this WebApplication app, string basePath) where T : Entity<TId>
    {
        app.MapGet(basePath, async (AppDbContext db) =>
        {
            return await db.Set<T>().ToListAsync();
        })
        .WithTags(basePath)
        .RequireAuthorization();

        app.MapGet($"{basePath}/{{id}}", async (TId id, AppDbContext db) =>
        {
            var entity = await db.Set<T>().FindAsync(id);
            if (entity is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(entity);
        })
        .WithTags(basePath)
        .RequireAuthorization();

        app.MapPost(basePath, async (T entity, AppDbContext db) =>
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();
            return Results.Created($"{basePath}/{entity.Id}", entity);
        })
        .WithTags(basePath)
        .RequireAuthorization();

        app.MapPut($"{basePath}/{{id}}", async (TId id, T entity, AppDbContext db) =>
        {
            var entityDb = await db.Set<T>().FindAsync(id);
            if (entityDb is null)
            {
                return Results.NotFound();
            }

            db.Entry(entityDb).CurrentValues.SetValues(entity);
            await db.SaveChangesAsync();
            return Results.Ok(entityDb);
        })
        .WithTags(basePath)
        .RequireAuthorization();

        app.MapDelete($"{basePath}/{{id}}", async (TId id, AppDbContext db) =>
        {
            var entity = await db.Set<T>().FindAsync(id);
            if (entity is null)
            {
                return Results.NotFound();
            }

            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithTags(basePath)
        .RequireAuthorization();
    }
}
