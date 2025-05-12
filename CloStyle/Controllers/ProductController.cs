using AutoMapper;
using CloStyle.Application.CloStyle.Commands.AddProduct;
using CloStyle.Application.CloStyle.Commands.DeleteProduct;
using CloStyle.Application.CloStyle.Dtos;
using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Application.CloStyle.Queries.GetAllGenders;
using CloStyle.Application.CloStyle.Queries.GetAllSizes;
using CloStyle.Application.CloStyle.Queries.GetBrandNameById;
using CloStyle.Application.CloStyle.Queries.GetCategoryById;
using CloStyle.Application.CloStyle.Queries.GetGenderById;
using CloStyle.Application.CloStyle.Queries.GetProductById;
using CloStyle.Application.CloStyle.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var command = new AddProductCommand
            {
                BrandId = id,   
                Sizes = (await _mediator.Send(new GetAllSizesQuery())).ToList(),
                Categories = (await _mediator.Send(new GetAllCategoriesQuery())).ToList(),
                Genders = (await _mediator.Send(new GetAllGendersQuery())).ToList(),
                BrandName = (await _mediator.Send(new GetBrandNameByIdQuery(id)))
            };

            return View(command);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                command.Categories = (await _mediator.Send(new GetAllCategoriesQuery())).ToList();
                command.Genders = (await _mediator.Send(new GetAllGendersQuery())).ToList();
                command.Sizes = (await _mediator.Send(new GetAllSizesQuery())).ToList();
                return View(command);
            }

            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(command.BrandId));
            await _mediator.Send(command);

            return Redirect($"/CloStyle/{brandName}/Products?brandId={command.BrandId}");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(product.BrandId));

            var model = new DeleteProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                BrandId = product.BrandId,
                BrandName = brandName,
                Description = product.Description
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            var brandName = await _mediator.Send(new GetBrandNameByIdQuery(command.BrandId));

            await _mediator.Send(command);
            return Redirect($"/CloStyle/{brandName}/Products?brandId={command.BrandId}");

        }
    }
}
