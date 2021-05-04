/***************************************************************
* Name        : Admin/Controllers/DepartmentController.cs
* Author      : Tom Sorteberg
* Created     : 05/01/2021
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
    public class DepartmentController : Controller
    {
        private Repository<Department> data { get; set; }
        public DepartmentController(PerfectTunesContext ctx)
        {
            data = new Repository<Department>(ctx);
        }

        public ViewResult Index(GridDTO vals)
        {
            string defaultSort = nameof(Department.Name);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<Department>
            {
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = d => d.Name;

            var vm = new DepartmentListViewModel
            {
                Departments = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        [HttpGet]
        public ViewResult Add() {
            var department = new Department();
            return View("Department", department);
        }

        [HttpPost]
        public IActionResult Add(Department department)
        {    
            var validate = new Validate(TempData);
            if (!validate.IsDepartmentChecked)
            {
                validate.CheckDepartment(department.DepartmentId, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(department.DepartmentId), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(department);
                data.Save();
                validate.ClearDepartment();
                TempData["message"] = $"{department.Name} added to Departments.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Department", department);
            }
            //string deptId = vm.Department.Name.Replace(' ', '_').ToLower();
            //vm.Department.DepartmentId = deptId;
            //data.Departments.Insert(vm.Department);
            //data.Save();
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Edit(string id) => View("Department", data.Get(id));

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                data.Update(department);
                data.Save();
                TempData["message"] = $"{department.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Department", department);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var department = data.Get(new QueryOptions<Department>
            {
                Includes = "Instruments",
                Where = d => d.DepartmentId == id
            });

            if (department.Instruments.Count > 0)
            {
                TempData["message"] = $"Can't delete department {department.Name} "
                                    + "because it's associated with these instruments.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Department", department);
            }
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            data.Delete(department);
            data.Save();
            TempData["message"] = $"{department.Name} removed from Genres.";
            return RedirectToAction("Index");
        }
    }
}