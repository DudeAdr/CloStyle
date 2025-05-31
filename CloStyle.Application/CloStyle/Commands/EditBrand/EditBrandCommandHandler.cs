using AutoMapper;
using CloStyle.Application.CurrentApplicationUser;
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
        private IUserContext _userContext;

        public EditBrandCommandHandler(IBrandRepository brandRepository, IFileRepository fileRepository, IUserContext userContext)
        {
            _brandRepository = brandRepository;
            _fileRepository = fileRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.Id!);
            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && (brand?.CreatedById == user.Id || (user.IsInRole("Admin")));

            if (!isEditable)
            {
                return Unit.Value;
            }

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            { 
                await _fileRepository.DeleteFileAsync(brand.ImgPath);
                request.ImgPath = await _fileRepository.SaveBrandImageAsync(request.ImageFile);
                brand.ImgPath = request.ImgPath;
            }

            brand.Name = request.Name;

            await _brandRepository.Commit();
            return Unit.Value;
        }
    }
}

