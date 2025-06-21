using CloStyle.Application.CloStyle.ViewModels.OwnerPanelVM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.OwnerPanelQueries.GetOwnerPanelData
{
    public class GetOwnerPanelDataQuery : IRequest<OwnerPanelDataVM>
    {
    }
}
