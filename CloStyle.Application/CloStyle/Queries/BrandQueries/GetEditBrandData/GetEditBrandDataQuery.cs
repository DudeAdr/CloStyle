using CloStyle.Application.CloStyle.Commands.EditBrand;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.BrandQueries.GetEditBrandData
{
    public record GetEditBrandDataQuery(int Id) : IRequest<EditBrandCommand>
    {
        public bool isEditable { get; set; }
    }
}
