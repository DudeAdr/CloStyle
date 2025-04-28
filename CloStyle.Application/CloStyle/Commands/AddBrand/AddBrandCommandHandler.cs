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
        private IFileRepository _fileRepository;

        public AddBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IFileRepository fileRepository)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            string imagePath = await _fileRepository.SaveBrandImageAsync(request.ImageFile);

            var brand = _mapper.Map<Brand>(request);
            brand.ImgPath = imagePath;

            await _brandRepository.Add(brand);
            return Unit.Value;
        }
    }

}
