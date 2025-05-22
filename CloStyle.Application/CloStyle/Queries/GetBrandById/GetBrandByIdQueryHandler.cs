using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetBrandById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
    {
        private IBrandRepository _repository;
        private IMapper _mapper;

        public GetBrandByIdQueryHandler(IBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _repository.GetBrandById(request.Id);
            var mappedBrand = _mapper.Map<BrandDto>(brand);
            return mappedBrand;
        }
    }
}
