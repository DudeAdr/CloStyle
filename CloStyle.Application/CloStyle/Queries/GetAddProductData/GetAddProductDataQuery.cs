using CloStyle.Application.CloStyle.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetAddProductData
{
    public record GetAddProductDataQuery(int id) : IRequest<AddProductViewModel>
    {
    }
}
