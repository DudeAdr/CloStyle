using CloStyle.Application.ApplicationUser;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.EditProduct
{
    class EditProductCommandHandler : IRequestHandler<EditProductCommand, Unit>
    {
        private IProductRepository _productRepository;
        private ISizeRepository _sizeRepository;
        private IUserContext _userContext;

        public EditProductCommandHandler(IProductRepository productRepository, ISizeRepository sizeRepository, IUserContext userContext)
        {
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.Id);
            var user = _userContext.GetCurrentUser();
            var isEditable = user != null && (product.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return Unit.Value;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.GenderId = request.GenderId;

            await _sizeRepository.RemoveProductSizes(product.Id);
            product.ProductSizes = new List<ProductSize>();

            foreach (var size in request.Sizes)
            {
                if (size.Stock <= 0)
                    continue;

                var sizeEntity = await _sizeRepository.GetSizeById(size.Id);
                if (sizeEntity == null)
                    continue;

                product.ProductSizes.Add(new ProductSize
                {
                    SizeId = sizeEntity.Id,
                    Size = sizeEntity,
                    Stock = size.Stock
                });
            }

            await _productRepository.Commit();
            return Unit.Value;
        }
    }
}
