using Microsoft.EntityFrameworkCore;
using DeployManager.Data;
using DeployManager.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace DeployManager.Controllers;

public static class ApplicationsController
{
    public static void MapApplicationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Application").WithTags(nameof(Application));

        group.MapGet("/", async (DeployManagerContext db) =>
        {
            return await db.Applications.ToListAsync();
        })
        .WithName("GetAllApplications")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Application>, NotFound>> (int id, DeployManagerContext db) =>
        {
            return await db.Applications.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Application model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetApplicationById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Application application, DeployManagerContext db) =>
        {
            var affected = await db.Applications
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Name, application.Name)
                    .SetProperty(m => m.Description, application.Description)
                );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateApplication")
        .WithOpenApi();


        group.MapPost("/", async (Application application, DeployManagerContext db) =>
        {
            db.Applications.Add(application);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Application/{application.Id}",application);
        })
        .WithName("CreateApplication")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, DeployManagerContext db) =>
        {
            var affected = await db.Applications
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteApplication")
        .WithOpenApi();
    }
}
