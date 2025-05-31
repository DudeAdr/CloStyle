using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.BrandQueries.GetEditBrandData
{
    public class GetEditBrandDataQueryHandler : IRequestHandler<GetEditBrandDataQuery, EditBrandCommand>
    {
        private IBrandRepository _brandRepository;
        private IUserContext _userContext;

        public GetEditBrandDataQueryHandler(IBrandRepository brandRepository, IUserContext userContext)
        {
            _brandRepository = brandRepository;
            _userContext = userContext;
        }
        public async Task<EditBrandCommand> Handle(GetEditBrandDataQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.Id);
            var user = _userContext.GetCurrentUser();

            var isEditable = user != null && (brand?.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                var error = new EditBrandCommand
                {
                    IsEditable = isEditable
                };
                return error;
            }

            var model = new EditBrandCommand
            {
                Id = request.Id,
                Name = brand.Name,
                BrandName = brand.Name,
                ImgPath = brand.ImgPath,
                IsEditable = isEditable
            };

            return model;
        }
    }
}
