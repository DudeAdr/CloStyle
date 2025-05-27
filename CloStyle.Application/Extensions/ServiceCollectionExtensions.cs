using AutoMapper;
using CloStyle.Application.ApplicationUser;
using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
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
            services.AddScoped<IUserContext, UserContext>();

            //automapper

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
                cfg.AddProfile(new BrandMappingProfile(userContext));
                cfg.AddProfile(new ProductMappingProfile(userContext));
                cfg.AddProfile(new GenderMappingProfile());
                cfg.AddProfile(new CategoryMappingProfile());
                cfg.AddProfile(new SizeMappingProfile());
            }).CreateMapper()
            );

            //validators
            services.AddValidatorsFromAssemblyContaining<AddBrandCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddScoped<IValidator<EditProductCommand>, EditProductCommandValidator>();
            services.AddScoped<IValidator<EditProductViewModel>, EditProductCommandValidator>();

            services.AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>();
            services.AddScoped<IValidator<AddProductViewModel>, AddProductCommandValidator>();

        }
    }
}
