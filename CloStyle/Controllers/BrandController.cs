 using AutoMapper;
using CloStyle.Application.CloStyle;
using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.CloStyle.Commands.DeleteBrand;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Application.CloStyle.Queries.GetAllBrands;
using CloStyle.Application.CloStyle.Queries.GetAllProducts;
using CloStyle.Application.CloStyle.Queries.GetBrandById;
using CloStyle.Application.CloStyle.Queries.GetBrandNameById;
using CloStyle.Application.CloStyle.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloStyle.Controllers
{
    public class BrandController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BrandController(IMediator brandService, IMapper mapper)
        {
            _mediator = brandService;
            _mapper = mapper;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBrandCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [Route("CloStyle/{brandName}/Products")]
        public async Task<IActionResult> Products(int brandId)
        {
            var model = await _mediator.Send(new GetAllProductsQuery(brandId));
            return View(model);
        }

        [HttpPost]
        [Route("CloStyle/{brandName}/Edit")]
        public async Task<IActionResult> Edit(EditBrandCommand command)
        {
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(command.Id));

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }


        [Route("CloStyle/{brandName}/Edit")]
        public async Task<IActionResult> Edit(int brandId)
        {
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(brandId));
            var brand = await _mediator.Send(new GetBrandByIdQuery(brandId));

            EditBrandCommand model = _mapper.Map<EditBrandCommand>(brand);
            return View(model);
        }

        [HttpPost]
        [Route("CloStyle/{brandName}/Delete")]
        public async Task<IActionResult> Delete(DeleteBrandCommand command)
        {
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(command.Id));

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Route("CloStyle/{brandName}/Delete")]
        public async Task<IActionResult> Delete(int brandId)
        {
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(brandId));
            var brand = await _mediator.Send(new GetBrandByIdQuery(brandId));
            DeleteBrandCommand model = _mapper.Map<DeleteBrandCommand>(brand);

            return View(model);
        }
        public async Task<IActionResult> Index()
        {
            var brand = await _mediator.Send(new GetAllBrandsQuery());
            return View(brand);
        }
    }
}
