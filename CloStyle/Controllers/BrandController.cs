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
        public async Task<IActionResult> Add(AddBrandCommand command, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            if (imageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "brands");

                var uniqueFileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                command.ImgPath = $"/images/brands/{uniqueFileName}";
            }
            else
            {
                command.ImgPath = null;
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
