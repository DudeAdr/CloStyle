using AutoMapper;
using CloStyle.Application.ApplicationUser;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.Mappings
{
    class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
