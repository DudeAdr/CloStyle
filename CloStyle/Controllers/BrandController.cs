using CloStyle.Application.CloStyle;
using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.CloStyle.Queries.GetAllBrands;
using CloStyle.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloStyle.Controllers
{
    public class BrandController : Controller
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator brandService)
        {
            _mediator = brandService;
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

        public async Task<IActionResult> Index()
        {
            var brand = await _mediator.Send(new GetAllBrandsQuery());
            return View(brand);
        }
    }
}
