using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetProductDetails
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsVM>
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private ICategoryRepository _categoryRepository;
        private IGenderRepository _genderRepository;
        private IBrandRepository _brandRepository;
        private ISizeRepository _sizeRepository;

        public GetProductDetailsQueryHandler(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository, IGenderRepository genderRepository, IBrandRepository brandRepository, ISizeRepository sizeRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _genderRepository = genderRepository;
            _brandRepository = brandRepository;
            _sizeRepository = sizeRepository;

        }
        public async Task<ProductDetailsVM> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.id);
            if (product == null)
            {
                return new ProductDetailsVM();
            }
            var gender = await _genderRepository.GetGenderById(product.GenderId);
            var category = await _categoryRepository.GetCategoryById(product.CategoryId);
            var sizes = await _sizeRepository.GetAll();

            var mappedProduct = _mapper.Map<ProductDetailsVM>(product);
            mappedProduct.Gender = _mapper.Map<GenderDto>(gender);
            mappedProduct.Category = _mapper.Map<CategoryDto>(category);
            mappedProduct.BrandName = await _brandRepository.GetBrandNameById(product.BrandId);

            mappedProduct.Sizes = sizes.Select(size => new SizeDto
            {
                Id = size.Id,
                Size = size.Name,
                Stock = product.ProductSizes.FirstOrDefault(ps => ps.SizeId == size.Id)?.Stock ?? 0
            }).ToList();

            return mappedProduct;
        }
    }
}
