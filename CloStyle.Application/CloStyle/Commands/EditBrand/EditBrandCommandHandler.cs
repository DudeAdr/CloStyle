using AutoMapper;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.EditBrand
{
    public class EditBrandCommandHandler : IRequestHandler<EditBrandCommand, Unit>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public EditBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.Id!);
            brand.Name = request.Name;
            brand.ImgPath = request.ImgPath;

            await _brandRepository.Commit();
            return Unit.Value;
        }
    }
}

