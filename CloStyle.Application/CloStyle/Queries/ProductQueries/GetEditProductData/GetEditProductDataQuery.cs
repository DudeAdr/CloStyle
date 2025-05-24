using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetEditProductData
{
    public record GetEditProductDataQuery(int id) : IRequest<EditProductViewModel>
    {
    }
}
