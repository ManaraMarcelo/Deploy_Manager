using Microsoft.EntityFrameworkCore;
using DeployManager.Data;
using DeployManager.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace DeployManager.Controllers;

public static class DeployController
{
    public static void MapDeployEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Deploy").WithTags(nameof(Deploy));

        group.MapGet("/", async (DeployManagerContext db) =>
        {
            return await db.Deploys.ToListAsync();
        })
        .WithName("GetAllDeploys")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Deploy>, NotFound>> (int id, DeployManagerContext db) =>
        {
            return await db.Deploys.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Deploy model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDeployById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Deploy deploy, DeployManagerContext db) =>
        {
            var affected = await db.Deploys
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, deploy.Id)
                    .SetProperty(m => m.ApplicationId, deploy.ApplicationId)
                    .SetProperty(m => m.EnvironmentId, deploy.EnvironmentId)
                    .SetProperty(m => m.DeployedAt, deploy.DeployedAt)
                    .SetProperty(m => m.Status, deploy.Status)
                    .SetProperty(m => m.Responsible, deploy.Responsible)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDeploy")
        .WithOpenApi();

        group.MapPost("/", async (Deploy deploy, DeployManagerContext db) =>
        {
            db.Deploys.Add(deploy);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Deploy/{deploy.Id}",deploy);
        })
        .WithName("CreateDeploy")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, DeployManagerContext db) =>
        {
            var affected = await db.Deploys
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDeploy")
        .WithOpenApi();
    }
}
