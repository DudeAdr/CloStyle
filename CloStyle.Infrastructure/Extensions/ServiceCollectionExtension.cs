using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using CloStyle.Infrastructure.Persistence;
using CloStyle.Infrastructure.Repositories;
using CloStyle.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
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

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CloStyleDbContext>();


            services.AddScoped<CloStyleSeeder>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IOwnerPanelRepository, OwnerPanelRepository>();
        }
    }
}
