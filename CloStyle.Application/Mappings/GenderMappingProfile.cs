﻿using AutoMapper;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.Mappings
{
    class GenderMappingProfile : Profile
    {
        public GenderMappingProfile()
        {
            CreateMap<Gender, GenderDto>();
            CreateMap<GenderDto, Gender>();
        }
    }
}
