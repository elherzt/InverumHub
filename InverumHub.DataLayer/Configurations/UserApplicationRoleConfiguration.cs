using InverumHub.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.DataLayer.Configurations
{
    public class UserApplicationRoleConfiguration : IEntityTypeConfiguration<UserApplicationRole>
    {
        public void Configure(EntityTypeBuilder<UserApplicationRole> builder)
        {
            builder.ToTable("user_application_roles");
            builder.HasKey(uar => new { uar.UserUid, uar.ApplicationId, uar.RoleId });

            builder.HasOne(uar => uar.User)
                   .WithMany(u => u.ApplicationRoles)
                   .HasForeignKey(uar => uar.UserUid)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uar => uar.Application)
                   .WithMany(a => a.UserRoles)
                   .HasForeignKey(uar => uar.ApplicationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uar => uar.Role)
                   .WithMany(r => r.UserApplication)
                   .HasForeignKey(uar => uar.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    
    }
}
