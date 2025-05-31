using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.Commands.DeleteBrand;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private IProductRepository _productRepository;
        private IUserContext _userContext;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUserContext userContext)
        {
            _productRepository = productRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var product = await _productRepository.GetProductById(request.Id);
            var isEditable = user != null && (product?.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            await _productRepository.DeleteProduct(request.Id);
            return Unit.Value;
        }
    }
}
