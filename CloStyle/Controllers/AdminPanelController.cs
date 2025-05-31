using CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUsersDataQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CloStyle.Controllers
{
    public class AdminPanelController : Controller
    {
        private IMediator _mediator;

        public AdminPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var userList = await _mediator.Send(new GetUsersDataQuery());
            return View(userList);
        }
    }
}
