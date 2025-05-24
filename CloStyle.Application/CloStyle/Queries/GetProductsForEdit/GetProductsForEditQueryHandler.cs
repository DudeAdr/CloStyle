using AutoMapper;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.Queries.GetBrandNameById;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetProductsForEdit
{
    public class GetProductsForEditQueryHandler : IRequestHandler<GetProductsForEditQuery, EditProductViewModel>
    {
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;
        private IGenderRepository _genderRepository;
        private ICategoryRepository _categoryRepository;
        private ISizeRepository _sizeRepository;
        private IMapper _mapper;

        public GetProductsForEditQueryHandler(IBrandRepository brandRepository, IProductRepository productRepository, IGenderRepository genderRepository, ICategoryRepository categoryRepository, ISizeRepository sizeRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _genderRepository = genderRepository;
            _categoryRepository = categoryRepository;
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<EditProductViewModel> Handle(GetProductsForEditQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var categories = await _categoryRepository.GetAll();
            var genders = await _genderRepository.GetAll();
            var allSizes = await _sizeRepository.GetAll();
            var brandName = await _brandRepository.GetBrandNameById(product.BrandId);

            var sizes = allSizes.Select(size => new SizeDto
            {
                Id = size.Id,
                Size = size.Name,
                Stock = product.ProductSizes.FirstOrDefault(ps => ps.SizeId == size.Id)?.Stock ?? 0
            }).ToList();

            var model = new EditProductViewModel
            {
                Id = request.id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                BrandId = product.BrandId,
                BrandName = brandName,
                CategoryId = product.CategoryId,
                GenderId = product.GenderId,
                Categories = _mapper.Map<List<CategoryDto>>(categories),
                Genders = _mapper.Map<List<GenderDto>>(genders),
                Sizes = sizes
            };

            return model;
        }
    }
}
