using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.DeleteProduct;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetAddProductData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetDeleteProductData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetEditProductData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetProductDetails;
using CloStyle.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Add(int id)
        {
            var model = await _mediator.Send(new GetAddProductDataQuery(id));
            if (!model.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Add(AddProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetAddProductDataQuery(command.Id));
                this.AddNotification("warning", $"Please check if every field of your form is filled properly");
                return View(model);
            }

            await _mediator.Send(command);

            this.AddNotification("success", $"Product {command.Name} added successfully!");

            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
        }

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _mediator.Send(new GetDeleteProductDataQuery(id));
            if (!model.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            await _mediator.Send(command);
            this.AddNotification("success", $"Product deleted successfully!");

            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
        }

        [HttpGet]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mediator.Send(new GetEditProductDataQuery(id));
            if (!model.IsEditable)
            {
                return RedirectToAction("NoAccess", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Owner,Admin")]
        public async Task<IActionResult> Edit(EditProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetEditProductDataQuery(command.Id));
                this.AddNotification("warning", $"Please check if every field of your form is filled properly");
                return View(model);
            }

            await _mediator.Send(command);
            this.AddNotification("success", $"Product {command.Name} edited successfully!");

            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var model = await _mediator.Send(new GetProductDetailsQuery(id));
            return View(model);
        }
    }
}
