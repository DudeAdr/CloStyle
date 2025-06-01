using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.BrandDTOs;
using CloStyle.Application.CloStyle.ViewModels.AdminPanelVM;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserBrandsQuery
{
    public class GetUserBrandsQueryHandler : IRequestHandler<GetUserBrandsQuery, ShowUserBrandsVM>
    {
        private IUserRepository _userRepository;
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public GetUserBrandsQueryHandler(IUserRepository userRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<ShowUserBrandsVM> Handle(GetUserBrandsQuery request, CancellationToken cancellationToken)
        {
            var userBrands = await _userRepository.GetUserBrandsAsync(request.Id);
            var mappedBrands = _mapper.Map<List<BrandDto>>(userBrands);

            return new ShowUserBrandsVM
            {
                UserId = request.Id,
                UserName = (await _userRepository.GetUserByIdAsync(request.Id))?.UserName,
                Brands = mappedBrands
            };

        }
    }
}
