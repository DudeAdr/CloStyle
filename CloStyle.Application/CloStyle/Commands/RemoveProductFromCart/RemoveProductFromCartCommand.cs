using CloStyle.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.RemoveProductFromCart
{
    public class RemoveProductFromCartCommand : IRequest<OperationResult>
    {
        public int ProductId { get; set; }
        public int ProductSizeId { get; set; }
    }
}
