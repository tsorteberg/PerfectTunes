/***************************************************************
* Name        : Admin/Controllers/InstrumentController.cs
* Author      : Tom Sorteberg
* Created     : 04/25/2021
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
using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PerfectTunes.Models;

namespace PerfectTunes.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class InstrumentController : Controller
    {
        private PerfectTunesUnitOfWork data { get; set; }
        private readonly IWebHostEnvironment hostingEnvironment;
        public InstrumentController(PerfectTunesContext ctx, IWebHostEnvironment hostingEnvironment) {
            data = new PerfectTunesUnitOfWork(ctx);
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index(InstrumentsGridDTO values)
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

        [HttpGet]
        public ViewResult Add(int id) => GetInstrument(id, "Add");

        [HttpPost]
        public IActionResult Add(InstrumentViewModel vm)
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
                    vm.Instrument.LogoImage = uniqueFileName;
                }
                else {
                    vm.Instrument.LogoImage = "comingsoon.jpg";
                }

                data.Instruments.Insert(vm.Instrument);
                data.Save();

                TempData["message"] = $"{vm.Instrument.Name} added to Instruments.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Instrument", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetInstrument(id, "Edit");

        [HttpPost]
        public IActionResult Edit(InstrumentViewModel vm)
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
                    vm.Instrument.LogoImage = uniqueFileName;
                }
                else
                {
                    vm.Instrument.LogoImage = "comingsoon.jpg";
                }

                data.Instruments.Update(vm.Instrument);
                data.Save();

                TempData["message"] = $"{vm.Instrument.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Edit");
                return View("Instrument", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetInstrument(id, "Delete");

        [HttpPost]
        public IActionResult Delete(InstrumentViewModel vm)
        {
            data.Instruments.Delete(vm.Instrument);
            data.Save();
            TempData["message"] = $"{vm.Instrument.Name} removed from Instruments.";
            return RedirectToAction("Index");
        }

        private ViewResult GetInstrument(int id, string operation)
        {
            var instrument = new InstrumentViewModel();
            Load(instrument, operation, id);
            return View("Instrument", instrument);
        }

        private void Load(InstrumentViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Instrument = new Instrument();
            }
            else
            {
                vm.Instrument = data.Instruments.Get(new QueryOptions<Instrument>
                {
                    Includes = "Brand, Department",
                    Where = i => i.InstrumentId == (id ?? vm.Instrument.InstrumentId)
                });
            }

            vm.Brands = data.Brands.List(new QueryOptions<Brand>
            {
                OrderBy = b => b.BrandName
            });
            vm.Departments = data.Departments.List(new QueryOptions<Department>
            {
                OrderBy = d => d.Name
            });
        }
    }
}