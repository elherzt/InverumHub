using InverumHub.Core.Entities;
using InverumHub.DataLayer.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.DataLayer.Context
{
    public class InverumDbContext : DbContext
    {
        public InverumDbContext(DbContextOptions<InverumDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Application> Applications => Set<Application>();
        public DbSet<Permission> Permissions => Set<Permission>();


        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserApplicationRole> UserApplicationRoles => Set<UserApplicationRole>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InverumDbContext).Assembly);

            ApplicationSeed.Seed(modelBuilder);
            RoleSeed.Seed(modelBuilder);
            PermissionSeed.Seed(modelBuilder);
            RolePermissionSeed.Seed(modelBuilder);

        }

    }
}
