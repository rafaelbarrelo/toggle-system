using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToggleSystem.Domain.Entities;
using ToggleSystem.Infra.Data.Mappings;

namespace ToggleSystem.Infra.Data.Context
{
    public class ToggleContext : IdentityDbContext
    {
        public DbSet<Toggle> Toggles { get; set; }

        public ToggleContext(DbContextOptions<ToggleContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToggleMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
