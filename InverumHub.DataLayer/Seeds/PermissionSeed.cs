using InverumHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.DataLayer.Seeds
{
    public class PermissionSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Code = "USER_READ", Description = "Read users" },
                new Permission { Id = 2, Code = "USER_WRITE", Description = "Write users" },
                new Permission { Id = 3, Code = "ROLE_READ", Description = "Read roles" },
                new Permission { Id = 4, Code = "ROLE_WRITE", Description = "Write roles" }
            );
        }
    }
}
