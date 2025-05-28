using AutoMapper;
using CloStyle.Application.ApplicationUser;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Commands.DeleteBrand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Unit>
    {
        private IBrandRepository _repository;
        private IFileRepository _fileRepository;
        private IUserContext _userContext;

        public DeleteBrandCommandHandler(IBrandRepository repository, IFileRepository fileRepository, IUserContext userContext)
        {
            _repository = repository;
            _fileRepository = fileRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var brand = await _repository.GetBrandById(request.Id);
            var isEditable = user != null && user.IsInRole("Admin");

            if (!isEditable)
            {
                return Unit.Value;
            }

            await _fileRepository.DeleteFileAsync(request.ImgPath);
            await _repository.Delete(request.Id);
            return Unit.Value;
        }
    }
}
    

