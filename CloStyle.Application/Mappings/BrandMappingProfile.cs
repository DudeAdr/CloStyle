using AutoMapper;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.Mappings
{
    public class BrandMappingProfile : Profile  
    {
        public BrandMappingProfile()
        {
            CreateMap<BrandDto, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, EditBrandCommand>();
        }
    }
}
