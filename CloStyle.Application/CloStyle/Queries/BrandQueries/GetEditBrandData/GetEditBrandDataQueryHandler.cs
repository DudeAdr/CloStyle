using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Domain.Interfaces;
using MediatR;
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
        public GetEditBrandDataQueryHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<EditBrandCommand> Handle(GetEditBrandDataQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetBrandById(request.Id);

            var model = new EditBrandCommand
            {
                Id = request.Id,
                Name = brand.Name,
                BrandName = brand.Name,
                ImgPath = brand.ImgPath,
            };

            return model;
        }
    }
}
