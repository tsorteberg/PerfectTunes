/***************************************************************
* Name        : HomeController.cs
* Author      : Tom Sorteberg
* Created     : 04/21/2021
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
using System;
using Microsoft.AspNetCore.Mvc;
using PerfectTunes.Models;

namespace PerfectTunes.Controllers
{
    public class HomeController : Controller
    {

        private Repository<Instrument> data { get; set; }
        public HomeController(PerfectTunesContext ctx) {
            data = new Repository<Instrument>(ctx);
        } 

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Instrument>
            {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
        }
    }
}