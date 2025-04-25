using CloStyle.Application.CloStyle.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.DeleteBrand
{
    class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, BrandDto>
    {
        public Task<BrandDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
    

