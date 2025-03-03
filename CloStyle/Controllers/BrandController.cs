using CloStyle.Application.Services;
using CloStyle.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CloStyle.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Brand brand)
        {
            await _brandService.Add(brand);
            return RedirectToAction(nameof(Add));
        }
    }
}
