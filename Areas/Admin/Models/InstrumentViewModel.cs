/***************************************************************
* Name        : Admin/Models/InstrumentViewModel.cs
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
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectTunes.Models
{
    public class InstrumentViewModel
    {
        public Instrument Instrument { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IFormFile Image { get; set; }
    }
}
