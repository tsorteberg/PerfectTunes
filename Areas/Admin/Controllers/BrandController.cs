/***************************************************************
* Name        : Admin/Controllers/InstrumentController.cs
* Author      : Tom Sorteberg
* Created     : 04/30/2021
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PerfectTunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectTunes.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {   
        private PerfectTunesUnitOfWork data { get; set; }
        private readonly IWebHostEnvironment hostingEnvironment;
        public BrandController(PerfectTunesContext ctx, IWebHostEnvironment hostingEnvironment)
        {
            data = new PerfectTunesUnitOfWork(ctx);
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index(GridDTO vals)
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
                options.OrderBy = b => b.BrandName;
            else
                options.OrderBy = p => p.ProductLine;

            var vm = new BrandListViewModel
            {
                Brands = data.Brands.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Brands.Count)
            };

            return View(vm);
        }
    }
}
