using CloStyle.Application.CloStyle.ViewModels.ShoppingCartVM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ShoppingCartQueries.GetShoppingCartDetails
{
    public class GetShoppingCartDetailsQuery : IRequest<ShoppingCartDetailsVM>
    {
    }
}
