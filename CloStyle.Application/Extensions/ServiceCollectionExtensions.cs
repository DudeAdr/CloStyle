using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.Queries.GetProductsForEdit;
using CloStyle.Application.CloStyle.ViewModels;
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
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddAutoMapper(typeof(GenderMappingProfile));
            services.AddAutoMapper(typeof(CategoryMappingProfile));
            services.AddAutoMapper(typeof(SizeMappingProfile));

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
