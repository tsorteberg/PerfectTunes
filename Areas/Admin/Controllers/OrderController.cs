/***************************************************************
* Name        : Areas/Admin/Controllers/InstrumentController.cs
* Author      : Tom Sorteberg
* Created     : 05/03/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access 
* to my program.         
***************************************************************/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectTunes.Models;

namespace PerfectTunes.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private Repository<Order> data { get; set; }
        public OrderController(PerfectTunesContext ctx)
        {
            data = new Repository<Order>(ctx);
        }

        public ViewResult Index(GridDTO vals)
        {
            string defaultSort = nameof(Order.OrderId);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Order>
            {
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = o => o.OrderId;

            var vm = new OrderListViewModel
            {
                Orders = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        [HttpGet]
        public ViewResult Update(int id) => View("Order", data.Get(id));

        [HttpPost]
        public IActionResult Update(Order order)
        {
            if (ModelState.IsValid)
            {   
                data.Update(order);
                data.Save();
                TempData["message"] = $"{order.OrderId} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Order", order);
            }
        }
    }
}
