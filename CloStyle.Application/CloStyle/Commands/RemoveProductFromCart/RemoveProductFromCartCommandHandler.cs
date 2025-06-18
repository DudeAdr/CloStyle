using CloStyle.Application.Common;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.RemoveProductFromCart
{
    public class RemoveProductFromCartCommandHandler : IRequestHandler<RemoveProductFromCartCommand, OperationResult>
    {
        private IShoppingCartRepository _shoppingCartRepository;
        private IUserContext _userContext;
        private IProductRepository _productRepository;

        public RemoveProductFromCartCommandHandler(IShoppingCartRepository shoppingCartRepository, IUserContext userContext, IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userContext = userContext;
            _productRepository = productRepository;
        }
        public async Task<OperationResult> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null)
            {
                return await Task.FromResult(OperationResult.Fail("Account"));
            }
            var product = await _productRepository.GetProductById(request.ProductId);
            await _shoppingCartRepository.RemoveItemFromShoppingCart(currentUser.Id, product, request.ProductSizeId);

            return await Task.FromResult(OperationResult.Ok());
        }
    }
}
