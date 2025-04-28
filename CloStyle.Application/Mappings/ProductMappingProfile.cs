using AutoMapper;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            CreateMap<ProductDto, Product>();
        }
    }
}
