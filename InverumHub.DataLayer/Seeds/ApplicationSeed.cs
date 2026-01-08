using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InverumHub.Core.Entities;

namespace InverumHub.DataLayer.Seeds
{
    public class ApplicationSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed initial data here if needed
            modelBuilder.Entity<Application>().HasData(
                new Application { Id = 1, Name = "Identity SSOT", Alias="ssot", Description="Enities Manager", IsActive=true  },
                new Application { Id = 2001, Name = "Customer Portal Web", Alias= "customer_web_app", Description="Front end portal for customers", IsActive=true },
                new Application { Id = 2002, Name = "Admin Backoffice", Alias= "admin_dashboard", Description= "Admin Backoffice", IsActive=true },
                new Application { Id = 2003, Name = "Mobile App", Alias= "mobile_app", Description="Android/iOS application", IsActive=true }
            );

        }
    }
}
