using InverumHub.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.DataLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInverumHubDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<InverumDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            return services;

        }
    }
}
