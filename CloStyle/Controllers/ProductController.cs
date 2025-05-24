using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.DeleteProduct;
using CloStyle.Application.CloStyle.Commands.EditProduct;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetAddProductData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetDeleteProductData;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetEditProductData;
using MediatR;
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
        public async Task<IActionResult> Add(int id)
        {
            var model = await _mediator.Send(new GetAddProductDataQuery(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetAddProductDataQuery(command.Id));
                return View(model);
            }

            await _mediator.Send(command);
            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _mediator.Send(new GetDeleteProductDataQuery(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            await _mediator.Send(command);
            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mediator.Send(new GetEditProductDataQuery(id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetEditProductDataQuery(command.Id));
                return View(model);
            }

            await _mediator.Send(command);
            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
        }
    }
}
