using Gateway.Data.Configurations;
using Gateway.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Data;

public class IdentityContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
    }
}   