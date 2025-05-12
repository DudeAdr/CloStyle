using CloStyle.Application.CloStyle.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetProductForDelete
{
    public record GetProductForDeleteQuery(int id) : IRequest<DeleteProductViewModel>
    {
    }
}
