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

        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private IGenderRepository _genderRepository;
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public AddProductCommandHandler(IProductRepository productRepository, IBrandRepository brandRepository, IGenderRepository genderRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _genderRepository = genderRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            product.BrandId = request.BrandId;
            product.Brand = await _brandRepository.GetBrandById(request.BrandId);
            product.Gender = await _genderRepository.GetGenderById(request.Gender.Id);
            product.Category = await _categoryRepository.GetCategoryById(request.Category.Id);
            await _productRepository.Add(product);
            return Unit.Value;
        }
    }
}
