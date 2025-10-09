using ef_mt_boilerplate.Models;
using Microsoft.EntityFrameworkCore;
using ef_mt_boilerplate.Services.Interface;
using System.Linq;
using ef_mt_boilerplate.entity;

namespace ef_mt_boilerplate.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantService _tenantService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService)
            : base(options)
        {
            _tenantService = tenantService;
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → Tenant
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Project → User (Owner)
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(p => p.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Project → Tenant
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Tenant)
                .WithMany(t => t.Projects)
                .HasForeignKey(p => p.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            var tenantId = _tenantService.GetCurrentTenantId();  // get current tenant

            // Automatically assign TenantId for new entities implementing ITenantEntity
            foreach (var entry in ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Added && e.Entity is ITenantEntity))
            {
                ((ITenantEntity)entry.Entity).TenantId = tenantId;
            }

            return base.SaveChanges();
        }
    }
}
