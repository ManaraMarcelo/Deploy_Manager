namespace DeployManager.Data
{
    using Microsoft.EntityFrameworkCore;
    using DeployManager.Models;

    public class DeployManagerContext : DbContext
    {
        public DeployManagerContext(DbContextOptions<DeployManagerContext> options) : base(options) { }

        public DbSet<Application> Applications { get; set; }
        public DbSet<DeploymentEnvironment> Environments { get; set; } = null!;
        public DbSet<Deploy> Deploys { get; set; }
    }

}
