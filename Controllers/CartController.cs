/***************************************************************
* Name        : PerfectTunes/Controllers/CartController.cs
* Author      : Tom Sorteberg
* Created     : 04/22/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project
* I have not used unauthorized source code, either modified or
* unmodified. I have not given other fellow student(s) access
* to my program.
***************************************************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerfectTunes.Models;
using PerfectTunes.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace PerfectTunes.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private Repository<Instrument> data { get; set; }
        private Repository<Order> repo { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartController(PerfectTunesContext ctx, IHttpContextAccessor httpContextAccessor) {
            data = new Repository<Instrument>(ctx);
            repo = new Repository<Order>(ctx);
            _httpContextAccessor = httpContextAccessor;
        }


        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public ViewResult Index()
        {
            Cart cart = GetCart();

            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            var vm = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                InstrumentGridRoute = builder.CurrentRoute
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var instrument = data.Get(new QueryOptions<Instrument>
            {
                Includes = "Brand, Department",
                Where = i => i.InstrumentId == id
            });
            if (instrument == null)
            {
                TempData["message"] = "Unable to add instrument to cart.";
            }
            else
            {
                var dto = new InstrumentDTO();
                dto.Load(instrument);
                CartItem item = new CartItem
                {
                    Instrument = dto,
                    Quantity = 1
                };

                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{instrument.Name} added to cart";
            }

            var builder = new InstrumentsGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Instrument", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Instrument.Name} removed from cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Instrument.Name} updated";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Checkout() {

            Cart cart = GetCart();

            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            var summary = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                InstrumentGridRoute = builder.CurrentRoute
            };

            var vm = new OrderViewModel();
            vm.Cart = summary;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel vm)
        {
            Cart cart = GetCart();

            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            var summary = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                InstrumentGridRoute = builder.CurrentRoute
            };

            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Order order = new Order();
                order.UserId = userId;
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (CartItem cartItem in cart.List) {
                    OrderItem orderItem = new OrderItem();
                    orderItem.Name = cartItem.Instrument.Name;
                    orderItem.Brand = cartItem.Instrument.Brand.BrandName;
                    orderItem.Quantity = cartItem.Quantity;
                    orderItem.Price = cartItem.Subtotal;
                    orderItems.Add(orderItem);
                }

                order.OrderItems = orderItems;
                order.OrderDate = DateTime.Now;
                order.FirstName = vm.FirstName;
                order.LastName = vm.LastName;
                order.Address = vm.Address;
                order.City = vm.City;
                order.State = vm.State;
                order.Zip = vm.Zip;
                order.Phone = vm.Phone;
                order.Email = vm.Email;
                order.CCName = vm.CCName;
                order.CCNum = vm.CCNum;
                order.CCMon = vm.CCMon;
                order.CCYear = vm.CCYear;
                order.CCCode = vm.CCCode;
                order.SubTotal = cart.Subtotal;
                order.Total = cart.Subtotal;
                order.Status = "Ordered";

                repo.Insert(order);
                data.Save();

                TempData["message"] = $"Your order has been placed.";

                return RedirectToAction("Index");
            }
            else {
                vm = new OrderViewModel();
                vm.Cart = summary;
                return View(vm);
            }
        }
    }
}