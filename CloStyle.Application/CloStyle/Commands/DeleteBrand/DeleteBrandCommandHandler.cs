using AutoMapper;
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

        public DeleteBrandCommandHandler(IBrandRepository repository, IFileRepository fileRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
        }
        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            await _fileRepository.DeleteFileAsync(request.ImgPath);
            await _repository.Delete(request.Id);
            return Unit.Value;
        }
    }
}
    

