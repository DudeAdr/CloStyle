using AutoMapper;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Application.CloStyle.Queries.GetCategoryById;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetGenderById
{
    public class GetGenderByIdQueryHandler : IRequestHandler<GetGenderByIdQuery, GenderDto>
    {
        private IGenderRepository _genderRepository;
        private IMapper _mapper;

        public GetGenderByIdQueryHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }
        public async Task<GenderDto> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken)
        {
            var gender = await _genderRepository.GetGenderById(request.id);
            var mappedGender = _mapper.Map<GenderDto>(gender);
            return mappedGender;
        }
    }
}
