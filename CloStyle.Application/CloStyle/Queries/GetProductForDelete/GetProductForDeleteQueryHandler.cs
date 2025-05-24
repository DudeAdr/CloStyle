using AutoMapper;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetProductForDelete
{
    public class GetProductForDeleteQueryHandler : IRequestHandler<GetProductForDeleteQuery, DeleteProductViewModel>
    {
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public GetProductForDeleteQueryHandler(IProductRepository productRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<DeleteProductViewModel> Handle(GetProductForDeleteQuery request, CancellationToken cancellationToken)
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
