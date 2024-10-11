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
            _localizer = localizer;
        }

        public ViewResult Index() => View(new Order());

        [HttpPost]
        public IActionResult Index(Order order)
        {
            if (!((Cart)_cart).Lines.Any())
            {
                ModelState.AddModelError(string.Empty, _localizer["CartEmpty"]);
            }
            if (string.IsNullOrWhiteSpace(order.Name))
            {
                ModelState.AddModelError(nameof(order.Name), _localizer["ErrorMissingName"]);
            }
            if (string.IsNullOrWhiteSpace(order.Address))
            {
                ModelState.AddModelError(nameof(order.Address), _localizer["ErrorMissingAddress"]);
            }
            if (string.IsNullOrWhiteSpace(order.City))
            {
                ModelState.AddModelError(nameof(order.City), _localizer["ErrorMissingCity"]);
            }
            if (string.IsNullOrWhiteSpace(order.Country))
            {
                ModelState.AddModelError(nameof(order.Country), _localizer["ErrorMissingCountry"]);
            }

            if (ModelState.IsValid)
            {
                order.Lines = (_cart as Cart)?.Lines.ToArray();
                _orderService.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }

            return View(order);
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }
    }
}