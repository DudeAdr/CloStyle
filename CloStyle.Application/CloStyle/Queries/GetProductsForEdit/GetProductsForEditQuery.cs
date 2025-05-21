using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetProductsForEdit
{
    public record GetProductsForEditQuery(int id) : IRequest<EditProductViewModel>
    {
    }
}
