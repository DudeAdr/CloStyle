using CloStyle.Application.CloStyle.Commands.ChangeUserPermissions;
using CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserBrandsQuery;
using CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUserRoleQuery;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userList = await _mediator.Send(new GetUsersDataQuery());
            return View(userList);
        }

        //MAYBE NOT NECESSARY
        /*public async Task<IActionResult> ShowUserBrands(string Id)
        {
            var userBrands = await _mediator.Send(new GetUserBrandsQuery(Id));
            return View(userBrands);
        }*/

        [HttpGet]
        public async Task<IActionResult> ChangeUserPermissions(string Id)
        {
            var model = await _mediator.Send(new GetUserRoleQuery(Id));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPermissions(ChangeUserPermissionsCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetUserRoleQuery(command.UserId));
                return View(model);
            }

            await _mediator.Send(command);
            return RedirectToAction("Index", "AdminPanel");
        }
    }
}
