using CloStyle.Application.CloStyle.Commands.AddProductToCart;
using CloStyle.Application.CloStyle.Commands.RemoveProductFromCart;
using CloStyle.Application.CloStyle.Queries.ProductQueries.GetProductDetails;
using CloStyle.Application.CloStyle.Queries.ShoppingCartQueries.GetShoppingCartDetails;
using CloStyle.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloStyle.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(AddProductToCartCommand command)
        {
            if (!ModelState.IsValid)
            {
                var model = await _mediator.Send(new GetProductDetailsQuery(command.Id));
                return View("~/Views/Product/ProductDetails.cshtml", model);
            }

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                switch (result.ErrorMessage)
                {
                    case "Stock":
                        this.AddNotification("error", $"Sorry, unfortunately, there are not that many pieces in stock");
                        return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
                    case "Account":
                        this.AddNotification("error", $"To add product to your cart please log in!");
                        return RedirectToAction("NoAccess", "Home");
                    default:
                        this.AddNotification("error", "Something went wrong. Please try again.");
                        return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");
                }
            }

            this.AddNotification("success", $"Product added successfully");
            return Redirect($"/CloStyle/{command.BrandName}/Products?brandId={command.BrandId}");

        }

        [HttpGet]
        public async Task<ActionResult> ShowShoppingCart()
        {
            var model = await _mediator.Send(new GetShoppingCartDetailsQuery());
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveProductFromCart(RemoveProductFromCartCommand command)
        {
            if(!ModelState.IsValid)
            {
                this.AddNotification("error", "Product not found");
                return RedirectToAction("ShowShoppingCart");
            }

            await _mediator.Send(command);
            this.AddNotification("success", "Product successfully removed from cart");
            return RedirectToAction("ShowShoppingCart");
        }
    }
}
