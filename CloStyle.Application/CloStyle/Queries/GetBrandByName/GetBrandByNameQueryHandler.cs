using AutoMapper;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetBrandByName
{
    public class GetBrandByNameQueryHandler : IRequestHandler<GetBrandByNameQuery, BrandDto>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public GetBrandByNameQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<BrandDto> Handle(GetBrandByNameQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandByName(request.Name);
            var mappedBrand = _mapper.Map<BrandDto>(brand);
            return mappedBrand;
        }
    }
}
