using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using CloStyle.Application.CloStyle.ViewModels.AdminPanelVM;
using CloStyle.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserBrandsQuery
{
    public record GetUserBrandsQuery(string Id) : IRequest<ShowUserBrandsVM>
    {
    }
}
