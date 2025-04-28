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
        private IFileRepository _fileRepository;

        public EditBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IFileRepository fileRepository)
        {
            _brandRepository = brandRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Unit> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.Id!);

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            { 
                await _fileRepository.DeleteFileAsync(brand.ImgPath);
                request.ImgPath = await _fileRepository.SaveBrandImageAsync(request.ImageFile);
            }

            brand.Name = request.Name;
            brand.ImgPath = request.ImgPath;

            await _brandRepository.Commit();

            return Unit.Value;
        }
    }
}

