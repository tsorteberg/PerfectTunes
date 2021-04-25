/***************************************************************
* Name        : BrandController.cs
* Author      : Tom Sorteberg
* Created     : 04/18/2021
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
using PerfectTunes.Models;
using Microsoft.AspNetCore.Mvc;

namespace PerfectTunes.Controllers
{
    public class BrandController : Controller
    {
        //private Repository<Brand> data { get; set; }
        //public BrandController(PerfectTunesContext ctx) => data = new Repository<Brand>(ctx);

        private PerfectTunesUnitOfWork data { get; set; }
        public BrandController(PerfectTunesContext ctx) => data = new PerfectTunesUnitOfWork(ctx);

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Brand.BrandName);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Brand>
            {
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.BrandName;
            else
                options.OrderBy = a => a.ProductLine;

            var vm = new BrandListViewModel
            {
                Brands = data.Brands.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Brands.Count)
            };

            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Shop(string id, bool clear = false)
        {
            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                string[] filter = { id, "any", "any" };
                var brand = data.Brands.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, brand);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", "Instrument", builder.CurrentRoute);
        }
    }
}
