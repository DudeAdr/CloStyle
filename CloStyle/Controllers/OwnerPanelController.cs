using CloStyle.Application.CloStyle.Queries.OwnerPanelQueries.GetOwnerPanelData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloStyle.MVC.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnerPanelController : Controller
    {
        private IMediator _mediator;

        public OwnerPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _mediator.Send(new GetOwnerPanelDataQuery());
            return View(model);
        }
    }
}
