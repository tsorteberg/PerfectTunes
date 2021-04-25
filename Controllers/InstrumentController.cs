/***************************************************************
* Name        : InstrumentController.cs
* Author      : Tom Sorteberg
* Created     : 04/21/2021
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
    public class InstrumentController : Controller
    {
        private PerfectTunesUnitOfWork data { get; set; }
        public InstrumentController(PerfectTunesContext ctx) => data = new PerfectTunesUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(InstrumentsGridDTO values)
        {
            var builder = new InstrumentsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Instrument.Name));

            var options = new InstrumentQueryOptions
            {
                Includes = "Brand, Department",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            options.SortFilter(builder);

            var vm = new InstrumentListViewModel
            {
                Instruments = data.Instruments.List(options),
                Brands = data.Brands.List(new QueryOptions<Brand>
                {
                    OrderBy = b => b.BrandName
                }),
                Departments = data.Departments.List(new QueryOptions<Department>
                {
                    OrderBy = d => d.Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Instruments.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var instrument = data.Instruments.Get(new QueryOptions<Instrument>
            {
                Includes = "Brand, Department",
                Where = i => i.InstrumentId == id
            });
            return View(instrument);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new InstrumentsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var brand = data.Brands.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, brand);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
