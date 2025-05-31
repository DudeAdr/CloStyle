using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Interfaces;
using MediatR;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetDeleteProductData
{
    public class GetDeleteProductDataQueryHandler: IRequestHandler<GetDeleteProductDataQuery, DeleteProductViewModel>
    {
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;
        private IUserContext _userContext;

        public GetDeleteProductDataQueryHandler(IProductRepository productRepository, IBrandRepository brandRepository, IUserContext userContext)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _userContext = userContext;
        }
        public async Task<DeleteProductViewModel> Handle(GetDeleteProductDataQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var product = await _productRepository.GetProductById(request.id);
            var brandName = await _brandRepository.GetBrandNameById(product.BrandId);
            var isEditable = user != null && (product?.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return new DeleteProductViewModel
                {
                    IsEditable = isEditable
                };
            }
            var model = new DeleteProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                BrandId = product.BrandId,
                BrandName = brandName,
                Description = product.Description,
                IsEditable = isEditable
            };
            return model;
        }
    }
}
