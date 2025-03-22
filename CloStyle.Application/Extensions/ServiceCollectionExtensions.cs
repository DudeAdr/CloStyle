using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.Mappings;
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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddBrandCommand>());

            //automapper
            services.AddAutoMapper(typeof(BrandMappingProfile));

            //validators
            services.AddValidatorsFromAssemblyContaining<AddBrandCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
