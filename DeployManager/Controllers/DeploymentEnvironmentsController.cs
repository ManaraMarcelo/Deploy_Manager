using Microsoft.EntityFrameworkCore;
using DeployManager.Data;
using DeployManager.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace DeployManager.Controllers;

public static class DeploymentEnvironmentsController
{
    public static void MapDeploymentEnvironmentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/DeploymentEnvironment").WithTags(nameof(DeploymentEnvironment));

        group.MapGet("/", async (DeployManagerContext db) =>
        {
            return await db.Environments.ToListAsync();
        })
        .WithName("GetAllDeploymentEnvironments")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<DeploymentEnvironment>, NotFound>> (int id, DeployManagerContext db) =>
        {
            return await db.Environments.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is DeploymentEnvironment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDeploymentEnvironmentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, DeploymentEnvironment deploymentEnvironment, DeployManagerContext db) =>
        {
            var affected = await db.Environments
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, deploymentEnvironment.Id)
                    .SetProperty(m => m.Name, deploymentEnvironment.Name)
                    .SetProperty(m => m.Description, deploymentEnvironment.Description)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDeploymentEnvironment")
        .WithOpenApi();

        group.MapPost("/", async (DeploymentEnvironment deploymentEnvironment, DeployManagerContext db) =>
        {
            db.Environments.Add(deploymentEnvironment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/DeploymentEnvironment/{deploymentEnvironment.Id}",deploymentEnvironment);
        })
        .WithName("CreateDeploymentEnvironment")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, DeployManagerContext db) =>
        {
            var affected = await db.Environments
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDeploymentEnvironment")
        .WithOpenApi();
    }
}
