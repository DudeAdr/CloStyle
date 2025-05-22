using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetAllSizes
{
    public class GetAllSizesQueryHandler : IRequestHandler<GetAllSizesQuery, IEnumerable<SizeDto>>
    {
        private ISizeRepository _sizeRepository;
        private IMapper _mapper;

        public GetAllSizesQueryHandler(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<SizeDto>> Handle(GetAllSizesQuery request, CancellationToken cancellationToken)
        {
            var sizes = await _sizeRepository.GetAll();
            var mappedSizes = _mapper.Map<IEnumerable<SizeDto>>(sizes);
            return mappedSizes;
        }
    }
}
