using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P2FixAnAppDotNetCode.Models;
using P2FixAnAppDotNetCode.Models.Services;

namespace P2FixAnAppDotNetCode.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICart _cart;
        private readonly IOrderService _orderService;
        private readonly IStringLocalizer<OrderController> _localizer;

        public OrderController(ICart pCart, IOrderService service, IStringLocalizer<OrderController> localizer)
        {
            _cart = pCart;
            _orderService = service;
            _localizer = localizer ?? throw new InvalidOperationException("Localization service not initialized.");
        }

        public ViewResult Index() => View(new Order());

        [HttpPost]
        public IActionResult Index(Order order)
        {
            // Check if the cart is empty
            if (!((Cart)_cart).Lines.Any())
            {
                ModelState.AddModelError(string.Empty, _localizer["CartEmpty"]);
            }

            // If the model state is valid (meaning all required fields are filled correctly)
            if (ModelState.IsValid)
            {
                // Assign lines from the cart to the order and save
                order.Lines = (_cart as Cart)?.Lines.ToArray();
                _orderService.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }

            // Return the view with the order object if validation fails
            return View(order);
        }

        public ViewResult Completed()
        {
            _cart.Clear(); // Clear the cart after order completion
            return View();
        }
    }
}
