using CloStyle.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddProductToCart
{
    public class AddProductToCartCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
