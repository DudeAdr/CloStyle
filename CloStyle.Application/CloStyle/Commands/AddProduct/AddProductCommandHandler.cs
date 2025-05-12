using AutoMapper;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(IProductRepository productRepository, IBrandRepository brandRepository, IGenderRepository genderRepository, ICategoryRepository categoryRepository, ISizeRepository sizeRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _genderRepository = genderRepository;
            _categoryRepository = categoryRepository;
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.GenderId = request.GenderId;

            product.Brand = await _brandRepository.GetBrandById(request.BrandId);
            product.Category = await _categoryRepository.GetCategoryById(request.CategoryId);
            product.Gender = await _genderRepository.GetGenderById(request.GenderId);

            product.ProductSizes = new List<ProductSize>();

            foreach (var sizeDto in request.Sizes)
            {
                if (sizeDto.Stock <= 0)
                    continue;

                var sizeEntity = await _sizeRepository.GetSizeById(sizeDto.Id);
                if (sizeEntity == null)
                    continue;

                product.ProductSizes.Add(new ProductSize
                {
                    SizeId = sizeEntity.Id,
                    Size = sizeEntity,
                    Stock = sizeDto.Stock
                });
            }

            await _productRepository.Add(product);
            return Unit.Value;
        }
    }
}

