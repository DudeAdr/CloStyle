using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Application.CloStyle.Queries.GetAllGenders;
using CloStyle.Application.CloStyle.Queries.GetAllSizes;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Interfaces;
using MediatR;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAddProductData
{
    public class GetAddProductDataQueryHandler : IRequestHandler<GetAddProductDataQuery, AddProductViewModel>
    {
        private IMediator _mediator;
        private IBrandRepository _repository;

        public GetAddProductDataQueryHandler(IMediator mediator, IBrandRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<AddProductViewModel> Handle(GetAddProductDataQuery request, CancellationToken cancellationToken)
        {
            var vm = new AddProductViewModel
            {
                BrandId = request.id ,
                Sizes = (await _mediator.Send(new GetAllSizesQuery())).ToList(),
                Categories = (await _mediator.Send(new GetAllCategoriesQuery())).ToList(),
                Genders = (await _mediator.Send(new GetAllGendersQuery())).ToList(),
                BrandName = await _repository.GetBrandNameById(request.id)
            };

            return vm;
        }
    }
}
