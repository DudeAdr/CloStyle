﻿using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    { 
        
    }
}
