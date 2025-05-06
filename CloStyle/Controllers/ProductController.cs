using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.DeleteProduct;
using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Application.CloStyle.Queries.GetAllGenders;
using CloStyle.Application.CloStyle.Queries.GetBrandNameById;
using CloStyle.Application.CloStyle.Queries.GetProductById;
using CloStyle.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CloStyle.Controllers
{
    public class ProductController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Add(int id)
        {
            var genders = await _mediator.Send(new GetAllGendersQuery());
            var categories = await _mediator.Send(new GetAllCategoriesQuery());

            ViewBag.BrandId = id;
            ViewBag.Genders = genders;
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductCommand command, int BrandId)
        {
            ViewBag.BrandId = BrandId;
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(BrandId));

            await _mediator.Send(command);
            return Redirect($"/CloStyle/{brandName}/Products?brandId={BrandId}");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            DeleteProductCommand model = _mapper.Map<DeleteProductCommand>(product);

            model.BrandId = product.BrandId;

            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(product.BrandId));
            ViewBag.BrandName = brandName;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductCommand command, int BrandId)
        {
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(BrandId));

            await _mediator.Send(command);

            return Redirect($"/CloStyle/{brandName}/Products?brandId={BrandId}");

        }
    }
}
