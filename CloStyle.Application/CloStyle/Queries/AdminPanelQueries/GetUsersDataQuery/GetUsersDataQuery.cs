using CloStyle.Application.CloStyle.Dtos.AdminPanelDTOs;
using CloStyle.Application.CloStyle.ViewModels.AdminPanelVM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUsersDataQuery
{
    public class GetUsersDataQuery : IRequest<IndexUsersVM>
    {
    }
}
