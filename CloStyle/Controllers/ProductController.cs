using AutoMapper;
using CloStyle.Application.CloStyle.Queries.GetAllCategories;
using CloStyle.Application.CloStyle.Queries.GetAllGenders;
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
        public async Task<IActionResult> Add()
        {
            var genders = await _mediator.Send(new GetAllGendersQuery());
            var categories = await _mediator.Send(new GetAllCategoriesQuery());

            ViewBag.Genders = genders;
            ViewBag.Categories = categories;

            return View();
        }

        /*[HttpPost]
        public IActionResult Add()
        {
            return View();
        }*/
    }
}
