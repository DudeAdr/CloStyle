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
        private IMapper _mapper;

        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.Add(product);
            return Unit.Value;
        }
    }
}
