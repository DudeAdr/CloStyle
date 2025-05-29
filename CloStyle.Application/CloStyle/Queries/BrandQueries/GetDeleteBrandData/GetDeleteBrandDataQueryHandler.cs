using AutoMapper;
using CloStyle.Application.CloStyle.Commands.DeleteBrand;
using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.BrandQueries.GetDeleteBrandData
{
    public class GetDeleteBrandDataQueryHandler : IRequestHandler<GetDeleteBrandDataQuery, DeleteBrandCommand>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public GetDeleteBrandDataQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<DeleteBrandCommand> Handle(GetDeleteBrandDataQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.Id);
            var mappedBrand = _mapper.Map<DeleteBrandCommand>(brand);
            return mappedBrand;
        }
    }
}
