using AutoMapper;
using CloStyle.Application.ApplicationUser;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;

namespace CloStyle.Application.CloStyle.Commands.AddBrand
{
    public class AddBrandCommandHandler : IRequestHandler<AddBrandCommand, Unit>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;
        private IFileRepository _fileRepository;
        private IUserContext _userContext;

        public AddBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, IFileRepository fileRepository, IUserContext userContext)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(AddBrandCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            if(currentUser == null || (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Owner")))
            {
                return Unit.Value;
            }

            string imagePath = await _fileRepository.SaveBrandImageAsync(request.ImageFile);

            var brand = _mapper.Map<Brand>(request);
            brand.ImgPath = imagePath;
            brand.CreatedById = currentUser.Id;

            await _brandRepository.Add(brand);
            return Unit.Value;
        }
    }

}
