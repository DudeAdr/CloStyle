using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Application.CloStyle.Queries.GetAllGenders;
using CloStyle.Application.CloStyle.Queries.GetAllSizes;
using CloStyle.Application.CloStyle.Queries.GetBrandNameById;
using CloStyle.Application.CloStyle.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.CloStyle.Queries.GetAddProductData
{
    public class GetAddProductDataQueryHandler : IRequestHandler<GetAddProductDataQuery, AddProductViewModel>
    {
        private IMediator _mediator;

        public GetAddProductDataQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<AddProductViewModel> Handle(GetAddProductDataQuery request, CancellationToken cancellationToken)
        {
            var vm = new AddProductViewModel
            {
                BrandId = request.id ,
                Sizes = (await _mediator.Send(new GetAllSizesQuery())).ToList(),
                Categories = (await _mediator.Send(new GetAllCategoriesQuery())).ToList(),
                Genders = (await _mediator.Send(new GetAllGendersQuery())).ToList(),
                BrandName = (await _mediator.Send(new GetBrandNameByIdQuery(request.id)))
            };

            return vm;
        }
    }
}
