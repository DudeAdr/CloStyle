using AutoMapper;
using CloStyle.Application.CloStyle.Dtos.ProductDTOs;
using CloStyle.Application.CloStyle.ViewModels.OwnerPanelVM;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.OwnerPanelQueries.GetOwnerPanelData
{
    public class GetOwnerPanelDataQueryHandler : IRequestHandler<GetOwnerPanelDataQuery, OwnerPanelDataVM>
    {
        private IUserContext _userContext;
        private IOwnerPanelRepository _ownerPanelRepository;
        private IMapper _mapper;

        public GetOwnerPanelDataQueryHandler(IUserContext userContext, IOwnerPanelRepository ownerPanelRepository, IMapper mapper)
        {
            _userContext = userContext;
            _ownerPanelRepository = ownerPanelRepository;
            _mapper = mapper;
        }
        public async Task<OwnerPanelDataVM> Handle(GetOwnerPanelDataQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            if (!user.IsInRole("Owner") || user == null)
            {
                return new OwnerPanelDataVM();
            }

            
            var ownerBrandAmout = await _ownerPanelRepository.GetOwnerBrandsAmount();
            var ownerProductsAmount = await _ownerPanelRepository.GetOwnerProductsAmount();
            var productsInUserCarts = await _ownerPanelRepository.GetProductsInUserCarts();
            var numberOfProductInUsersCarts = await _ownerPanelRepository.GetNumberOfProductInUsersCarts();
            //REFACTOR LATER AFTER implementing Orders and pdf receipts
            var totalIncome = await _ownerPanelRepository.GetTotalIncome();

            var model = new OwnerPanelDataVM
            {
                OwnerBrandAmount = ownerBrandAmout,
                OwnerProductsAmount = ownerProductsAmount,
                ProductsInUserCarts = _mapper.Map<List<ProductForShoppingCartDto>>(productsInUserCarts),
                TotalIncome = totalIncome,
                NumberOfProductInUsersCarts = numberOfProductInUsersCarts
            };

            return model;

        }
    }
}
