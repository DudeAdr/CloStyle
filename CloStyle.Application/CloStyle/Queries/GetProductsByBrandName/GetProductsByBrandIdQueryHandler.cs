using AutoMapper;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetProductsByBrandName
{
    public class GetProductsByBrandIdQueryHandler : IRequestHandler<GetProductsByBrandIdQuery, IEnumerable<ProductDto>>
    {
        private IProductRepository _repository;
        private IMapper _mapper;

        public GetProductsByBrandIdQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsByBrandIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetByBrandId(request.BrandId);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
