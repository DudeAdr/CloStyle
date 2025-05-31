using CloStyle.Application.CurrentApplicationUser;
using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Application.CloStyle.Queries.GetAllGenders;
using CloStyle.Application.CloStyle.Queries.GetAllSizes;
using CloStyle.Application.CloStyle.ViewModels.ProductVM;
using CloStyle.Domain.Entities;
using CloStyle.Domain.Interfaces;
using MediatR;

namespace CloStyle.Application.CloStyle.Queries.ProductQueries.GetAddProductData
{
    public class GetAddProductDataQueryHandler : IRequestHandler<GetAddProductDataQuery, AddProductViewModel>
    {
        private IMediator _mediator;
        private IBrandRepository _repository;
        private IUserContext _userContext;

        public GetAddProductDataQueryHandler(IMediator mediator, IBrandRepository repository, IUserContext userContext)
        {
            _mediator = mediator;
            _repository = repository;
            _userContext = userContext;
        }
        public async Task<AddProductViewModel> Handle(GetAddProductDataQuery request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var brand = await _repository.GetBrandById(request.id);
            var isEditable = user != null && (brand?.CreatedById == user.Id || user.IsInRole("Admin"));

            if (!isEditable)
            {
                return new AddProductViewModel
                {
                    IsEditable = isEditable,
                };
            }
            var vm = new AddProductViewModel
            {
                BrandId = request.id ,
                Sizes = (await _mediator.Send(new GetAllSizesQuery())).ToList(),
                Categories = (await _mediator.Send(new GetAllCategoriesQuery())).ToList(),
                Genders = (await _mediator.Send(new GetAllGendersQuery())).ToList(),
                BrandName = await _repository.GetBrandNameById(request.id),
                IsEditable = isEditable
            };

            return vm;
        }
    }
}
