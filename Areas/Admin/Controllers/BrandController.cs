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
using System.IO;
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

        [HttpGet]
        public ViewResult Add(int id) => GetBrand(id, "Add");

        [HttpPost]
        public IActionResult Add(BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (vm.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    vm.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    vm.Brand.LogoImage = uniqueFileName;
                }
                else
                {
                    vm.Brand.LogoImage = "comingsoon.jpg";
                }

                data.Brands.Insert(vm.Brand);
                data.Save();

                TempData["message"] = $"{vm.Brand.BrandName} added to Instruments.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Brand", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetBrand(id, "Edit");

        [HttpPost]
        public IActionResult Edit(BrandViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (vm.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    vm.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    vm.Brand.LogoImage = uniqueFileName;
                }
                else
                {
                    vm.Brand.LogoImage = "comingsoon.jpg";
                }

                data.Brands.Update(vm.Brand);
                data.Save();

                TempData["message"] = $"{vm.Brand.BrandName} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Edit");
                return View("Instrument", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetBrand(id, "Delete");

        [HttpPost]
        public IActionResult Delete(BrandViewModel vm)
        {
            data.Brands.Delete(vm.Brand);
            data.Save();
            TempData["message"] = $"{vm.Brand.BrandName} removed from Brands.";
            return RedirectToAction("Index");
        }

        private ViewResult GetBrand(int id, string operation)
        {
            var brand = new BrandViewModel();
            Load(brand, operation, id);
            return View("Brand", brand);
        }

        private void Load(BrandViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Brand = new Brand();
            }
            else
            {
                vm.Brand = data.Brands.Get(new QueryOptions<Brand>
                {
                    Where = b => b.BrandId == (id ?? vm.Brand.BrandId)
                });
            }
        }
    }
}
