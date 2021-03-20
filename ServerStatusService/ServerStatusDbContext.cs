using Microsoft.EntityFrameworkCore;

namespace ServerStatusService
{
    public class ServerStatusDbContext : DbContext
    {
        public DbSet<ServerStatus> Statuses { get; set; }

        public ServerStatusDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServerStatusConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}