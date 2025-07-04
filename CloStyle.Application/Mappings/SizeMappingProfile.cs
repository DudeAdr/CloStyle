﻿using AutoMapper;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllSizes;

namespace CloStyle.Application.Mappings
{
    public class SizeMappingProfile : Profile
    {
        public SizeMappingProfile()
        {
            CreateMap<Size, SizeDto>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Name));

            CreateMap<SizeDto, ProductSize>()
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
            .ForMember(dest => dest.Size, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<GetAllSizesQuery, SizeDto>();
        }
    }
}
