using CloStyle.Application.CloStyle.Commands.DeleteBrand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.BrandQueries.GetDeleteBrandData
{
    public record GetDeleteBrandDataQuery(int Id) : IRequest<DeleteBrandCommand>
    {
    }
}
