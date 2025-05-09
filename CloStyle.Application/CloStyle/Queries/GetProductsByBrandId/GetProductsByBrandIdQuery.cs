using CloStyle.Application.CloStyle.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetProductsByBrandName
{
    public record GetProductsByBrandIdQuery(int BrandId) : IRequest<IEnumerable<ProductDto>>
    {
    }
}
