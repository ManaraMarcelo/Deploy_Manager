using Microsoft.EntityFrameworkCore;
using DeployManager.Models; // ou o namespace correto
using DeployManager.Data;
using DeployManager.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DeployManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapApplicationEndpoints();

app.MapDeployEndpoints();

app.MapDeploymentEnvironmentEndpoints();

app.Run();
