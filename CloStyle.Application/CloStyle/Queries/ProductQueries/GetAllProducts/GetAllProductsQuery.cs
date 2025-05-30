﻿using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllProducts
{
    public record GetAllProductsQuery(int brandId) : IRequest<ProductsByBrandViewModel>
    {
    }
}
