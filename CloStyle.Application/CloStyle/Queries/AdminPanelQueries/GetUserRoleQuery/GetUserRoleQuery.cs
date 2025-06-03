using CloStyle.Application.CloStyle.ViewModels.AdminPanelVM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserRoleQuery
{
    public record GetUserRoleQuery(string Id) : IRequest<ShowUserRolesVM>
    {
    }
}
