using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllGenders
{
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, IEnumerable<GenderDto>>
    {
        private IGenderRepository _genderRepository;
        private IMapper _mapper;
        public GetAllGendersQueryHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GenderDto>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            var genders = await _genderRepository.GetAll();
            var mappedGenders = _mapper.Map<IEnumerable<GenderDto>>(genders);
            return mappedGenders;
        }
    }
}

