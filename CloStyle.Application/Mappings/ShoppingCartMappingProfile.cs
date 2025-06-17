using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.ViewModels.ShoppingCartVM;
using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.Mappings
{
    public class ShoppingCartMappingProfile : Profile
    {
        public ShoppingCartMappingProfile()
        {
            CreateMap<ProductForShoppingCartDto, ShoppingCartDetailsVM>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => new List<ProductForShoppingCartDto> { src }))
                .ForMember(dest => dest.TotalCartPrice, opt => opt.Ignore());

            CreateMap<ShoppingCartItem, ProductForShoppingCartDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Product.Brand.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ProductSizeId, opt => opt.MapFrom(src => src.ProductSizeId))
                .ForMember(dest => dest.ProductSizeName, opt => opt.MapFrom(src => src.ProductSize.Size.Name));

            CreateMap<ShoppingCart, ShoppingCartDetailsVM>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.TotalCartPrice, opt => opt.MapFrom(src => src.TotalPrice));


        }
    }
}
