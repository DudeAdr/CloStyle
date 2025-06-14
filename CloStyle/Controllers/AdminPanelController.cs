﻿using CloStyle.Application.CloStyle.Commands.ChangeUserPermissions;
using CloStyle.Application.CloStyle.Commands.DeleteUser;
using CloStyle.Application.CloStyle.Queries.AdminPanelQueries.GetUsersDataQuery;
using CloStyle.Extensions;
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

        [HttpPost]
        public async Task<IActionResult> ChangeUserPermissions(ChangeUserPermissionsCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.AddNotification("error", $"Account cannot have no roles!");
                return RedirectToAction("Index", "AdminPanel");
            }

            await _mediator.Send(command);
            return RedirectToAction("Index", "AdminPanel");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
        {
            var isDeleted = await _mediator.Send(command);
            if (!isDeleted)
            {
                this.AddNotification("error", $"You can't delete your own account!");
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                this.AddNotification("success", $"Account has been deleted successfully!");
                return RedirectToAction("Index", "AdminPanel");
            }
                
        }
    }
}
