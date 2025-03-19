using CloStyle.Application.CloStyle;
using CloStyle.Application.Mappings;
using CloStyle.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace CloStyle.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            //services
            services.AddScoped<IBrandService, BrandService>();

            //automapper
            services.AddAutoMapper(typeof(BrandMappingProfile));

            //validators
            services.AddValidatorsFromAssemblyContaining<BrandDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
