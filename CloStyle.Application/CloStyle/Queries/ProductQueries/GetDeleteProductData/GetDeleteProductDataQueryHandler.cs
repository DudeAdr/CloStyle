using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Interfaces;
using MediatR;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetDeleteProductData
{
    public class GetDeleteProductDataQueryHandler: IRequestHandler<GetDeleteProductDataQuery, DeleteProductViewModel>
    {
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;

        public GetDeleteProductDataQueryHandler(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }
        public async Task<DeleteProductViewModel> Handle(GetDeleteProductDataQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.id);
            var brandName = await _brandRepository.GetBrandNameById(product.BrandId);

            var model = new DeleteProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                BrandId = product.BrandId,
                BrandName = brandName,
                Description = product.Description
            };
            return model;
        }
    }
}
