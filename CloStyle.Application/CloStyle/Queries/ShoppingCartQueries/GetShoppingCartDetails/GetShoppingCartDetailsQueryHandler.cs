using AutoMapper;
using CloStyle.Application.CloStyle.ViewModels.ShoppingCartVM;
using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.ShoppingCartQueries.GetShoppingCartDetails
{
    public class GetShoppingCartDetailsQueryHandler : IRequestHandler<GetShoppingCartDetailsQuery, ShoppingCartDetailsVM>
    {
        private IUserContext _userContext;
        private IShoppingCartRepository _shoppingCartRepository;
        private IMapper _mapper;

        public GetShoppingCartDetailsQueryHandler(IUserContext userContext, IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _userContext = userContext;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartDetailsVM> Handle(GetShoppingCartDetailsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if(currentUser == null)
            {
                return new ShoppingCartDetailsVM();
            }
            var cart = await _shoppingCartRepository.GetShoppingCartByUserId(currentUser.Id);      
            var mapped = _mapper.Map<ShoppingCartDetailsVM>(cart);
            return mapped;
        }
    }
}
