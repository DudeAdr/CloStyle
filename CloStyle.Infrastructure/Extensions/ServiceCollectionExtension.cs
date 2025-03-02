using CloStyle.Infrastructure.Persistence;
using CloStyle.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CloStyleDbContext>(opt => opt.UseSqlServer(
                configuration.GetConnectionString("CloStyle")));

            services.AddScoped<CloStyleSeeder>();
        }
    }
}
