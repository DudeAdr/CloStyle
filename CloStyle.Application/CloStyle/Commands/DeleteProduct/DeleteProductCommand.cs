using CloStyle.Application.CloStyle.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool IsEditable { get; set; }
    }
}
