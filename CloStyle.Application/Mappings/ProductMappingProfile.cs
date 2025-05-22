using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.DeleteProduct;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.ViewModels;
using CloStyle.Domain.Entities;


namespace CloStyle.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.Category.Name))
            .ForPath(dest => dest.Gender.Name, opt => opt.MapFrom(src => src.Gender.Name))
            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src =>
            src.ProductSizes.Select(ps => new SizeDto
            {
                Size = ps.Size.Name,
                Stock = ps.Stock
            })));

            CreateMap<ProductDto, DeleteProductCommand>();
            CreateMap<AddProductCommand, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<EditProductViewModel, EditProductCommand>();

        }
    }
}
