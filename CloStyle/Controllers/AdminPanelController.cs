using CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserBrandsQuery;
using CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUsersDataQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloStyle.Controllers
{
    [Authorize(Roles = "Admin")]
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

        //MAYBE NOT NECESSARY
        public async Task<IActionResult> ShowUserBrands(string Id)
        {
            var userBrands = await _mediator.Send(new GetUserBrandsQuery(Id));
            return View(userBrands);
        }

        public async Task<IActionResult> ChangeUserPermissions()
        {
            var userList = await _mediator.Send(new GetUsersDataQuery());
            return View(userList);
        }
    }
}
