using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.CloStyle.Commands.DeleteBrand;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Application.CloStyle.Queries.BrandQueries.GetAllBrands;
using CloStyle.Application.CloStyle.Queries.BrandQueries.GetBrandById;
using CloStyle.Application.CloStyle.Queries.BrandQueries.GetEditBrandData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Add(AddBrandCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("CloStyle/{brandName}/Products")]
        public async Task<IActionResult> Products(int brandId)
        {
            var model = await _mediator.Send(new GetAllProductsQuery(brandId));
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        [Route("CloStyle/{brandName}/Edit")]
        public async Task<IActionResult> Edit(int brandId)
        {
            var model = await _mediator.Send(new GetEditBrandDataQuery(brandId));

            if (!model.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [Route("CloStyle/{brandName}/Edit")]
        public async Task<IActionResult> Edit(EditBrandCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetEditBrandDataQuery(command.Id));
                return View(model);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        [Route("CloStyle/{brandName}/Delete")]
        public async Task<IActionResult> Delete(int brandId)
        {
            var brand = await _mediator.Send(new GetBrandByIdQuery(brandId));
            DeleteBrandCommand model = _mapper.Map<DeleteBrandCommand>(brand);

            if (!model.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        [Route("CloStyle/{brandName}/Delete")]
        public async Task<IActionResult> Delete(DeleteBrandCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var brand = await _mediator.Send(new GetAllBrandsQuery());
            return View(brand);
        }
    }
}
