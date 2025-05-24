using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.BrandQueries.GetBrandById
{
    public record GetBrandByIdQuery (int Id) : IRequest<BrandDto>
    {
    }
}
