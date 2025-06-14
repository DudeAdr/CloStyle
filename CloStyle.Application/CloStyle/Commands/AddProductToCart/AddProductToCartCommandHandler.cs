using CloStyle.Application.Common;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddProductToCart
{
    public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand, OperationResult>
    {
        private IUserContext _userContext;
        private IShoppingCartRepository _shoppingCartRepository;
        private IProductRepository _productRepository;

        public AddProductToCartCommandHandler(IUserContext userContext, IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _userContext = userContext;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }
        public async Task<OperationResult> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.IsInRole("User"))
            {
                return OperationResult.Fail("Account");
            }

            var product = await _productRepository.GetProductById(request.Id);
            try
            {
                await _shoppingCartRepository.AddItemToShoppingCart(currentUser.Id, product, request.SizeId, request.Quantity);
            }
            catch (InvalidOperationException)
            {
                return OperationResult.Fail("Stock");
            }

            return OperationResult.Ok();
        }
    }
}
