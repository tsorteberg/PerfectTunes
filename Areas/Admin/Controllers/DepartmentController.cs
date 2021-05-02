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
        private PerfectTunesUnitOfWork data { get; set; }
        private Repository<Department> repo { get; set; }
        public DepartmentController(PerfectTunesContext ctx)
        {
            data = new PerfectTunesUnitOfWork(ctx);
            repo = new Repository<Department>(ctx);
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
                Departments = data.Departments.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Departments.Count)
            };

            return View(vm);
        }

        [HttpGet]
        public ViewResult Add() {
            var department = new DepartmentViewModel();
            return View("Department", department);
        }

        [HttpPost]
        public IActionResult Add(DepartmentViewModel vm)
        {
            // server-side version of remote validation
            
            var validate = new Validate(TempData);
            if (!validate.IsDepartmentChecked)
            {
                validate.CheckDepartment(vm.Department.DepartmentId, repo);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(vm.Department.DepartmentId), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Departments.Insert(vm.Department);
                data.Save();
                validate.ClearDepartment();
                TempData["message"] = $"{vm.Department.Name} added to Departments.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Department", vm);
            }
            //string deptId = vm.Department.Name.Replace(' ', '_').ToLower();
            //vm.Department.DepartmentId = deptId;
            //data.Departments.Insert(vm.Department);
            //data.Save();
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Edit(string id) => GetDepartment(id, "Edit");

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel vm)
        {
            if (ModelState.IsValid)
            {

                data.Departments.Update(vm.Department);
                data.Save();

                TempData["message"] = $"{vm.Department.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Edit");
                return View("Instrument", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(string id) => GetDepartment(id, "Delete");

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel vm)
        {
            data.Departments.Delete(vm.Department);
            data.Save();
            TempData["message"] = $"{vm.Department.Name} removed from Departments.";
            return RedirectToAction("Index");
        }

        private ViewResult GetDepartment(string id, string operation)
        {
            var department = new DepartmentViewModel();
            Load(department, operation, id);
            return View("Department", department);
        }

        private void Load(DepartmentViewModel vm, string op, string id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Department = new Department();
            }
            else
            {
                vm.Department = data.Departments.Get(new QueryOptions<Department>
                {
                    Where = d => d.DepartmentId == vm.Department.DepartmentId
                });
            }
        }
    }
}