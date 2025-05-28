using CloStyle.Application.ApplicationUser;
using CloStyle.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.BrandQueries.CanUserAddBrandByAmount
{
    public class CanUserAddBrandByAmountQueryHandler : IRequestHandler<CanUserAddBrandByAmountQuery, bool>
    {
        private IBrandRepository _brandRepository;
        private IUserContext _userContext;

        public CanUserAddBrandByAmountQueryHandler(IBrandRepository brandRepository, IUserContext userContext)
        {
            _brandRepository = brandRepository;
            _userContext = userContext;
        }
        public async Task<bool> Handle(CanUserAddBrandByAmountQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            
            if(currentUser == null)
            {
                return false;
            }
            if (currentUser.IsInRole("Admin"))
            {
                return true;
            }

            var brandCount = await _brandRepository.GetUserBrandAmount(currentUser.Id);
            return currentUser.HasSpaceForBrand(brandCount);
        }
    }
}
