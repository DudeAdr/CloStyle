using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddBrand;
using CloStyle.Application.CloStyle.Commands.DeleteBrand;
using CloStyle.Application.CloStyle.Commands.EditBrand;
using CloStyle.Application.CloStyle.Queries.BrandQueries.CanUserAddBrandByAmount;
using CloStyle.Application.CloStyle.Queries.BrandQueries.GetAllBrands;
using CloStyle.Application.CloStyle.Queries.BrandQueries.GetDeleteBrandData;
using CloStyle.Application.CloStyle.Queries.BrandQueries.GetEditBrandData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetAllProducts;
using CloStyle.Extensions;
using CloStyle.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CloStyle.Controllers
{
    public class BrandController : Controller
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator brandService)
        {
            _mediator = brandService; 
        }

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Add()
        {
            var canAddBrand = await _mediator.Send(new CanUserAddBrandByAmountQuery());
            if(!canAddBrand)
            {
                return RedirectToAction("NoAddBrandAccess", "Home");
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Add(AddBrandCommand command)
        {
            var canAddBrand = await _mediator.Send(new CanUserAddBrandByAmountQuery());

            if (!ModelState.IsValid)
            {
                this.AddNotification("warning", $"Please check if every field of your form is filled properly");
                return View(command);
            }
            
            if (!canAddBrand)
            {
                return RedirectToAction("NoAddBrandAccess", "Home");
            }

            await _mediator.Send(command);
            this.AddNotification("success", $"Brand {command.Name} added successfully!");

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
            this.AddNotification("success", $"Brand {command.Name} edited successfully!");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("CloStyle/{brandName}/Delete")]
        public async Task<IActionResult> Delete(int brandId)
        {
            var model = await _mediator.Send(new GetDeleteBrandDataQuery(brandId));

            if (!model.IsEditable || !User.IsInRole("Admin"))
            {
                return RedirectToAction("NoDeleteAccess", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("CloStyle/{brandName}/Delete")]
        public async Task<IActionResult> Delete(DeleteBrandCommand command)
        {
            await _mediator.Send(command);
            this.AddNotification("success", $"Brand {command.Name} deleted successfully!");

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
