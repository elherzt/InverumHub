using InverumHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.DataLayer.Seeds
{
    public class RoleSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed initial data here if needed
            modelBuilder.Entity<Role>().HasData(    
                new Role { Id = 1, Name = "Administrator", Description = "Full access to all system features and settings." },
                new Role { Id = 2, Name = "User", Description = "Standard user with limited access to features."},
                new Role { Id = 3, Name = "Guest", Description = "Read-only access to public information." },
                new Role { Id = 999, Name = "SSOT_ADMIN", Description = "Root Admin" }
            );
        }
    }
}
