using AutoMapper;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.AddBrand
{
    public class AddBrandCommandHandler : IRequestHandler<AddBrandCommand, Unit>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public AddBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Brand>(request);
            await _brandRepository.Add(brand);

            return Unit.Value;
        }
    }

}
