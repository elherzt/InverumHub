using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InverumHub.Core.Entities;

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
        }

    }
}
