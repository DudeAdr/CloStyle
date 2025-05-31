using AutoMapper;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsByBrandViewModel>
    {
        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private IMapper _mapper;
        private IUserContext _userContext;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IBrandRepository brandRepository, IMapper mapper, IUserContext userContext)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<ProductsByBrandViewModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var products = await _productRepository.GetByBrandId(request.brandId);
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);
            var brandName = await _brandRepository.GetBrandNameById(request.brandId);
            var isEditable = user != null && (products.FirstOrDefault()?.Brand.CreatedById == user.Id || user.IsInRole("Admin"));

            return new ProductsByBrandViewModel
            {
                BrandId = request.brandId,
                BrandName = brandName,
                Products = mappedProducts,
                IsEditable = isEditable
            };
        }
    }
}
