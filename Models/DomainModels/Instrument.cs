/***************************************************************
* Name        : Instrument.cs
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
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectTunes.Models
{
    public class Instrument
    {
        public int InstrumentId { get; set; }

        [Required(ErrorMessage = "Please enter an instrument name.")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a department.")]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Please select a brand.")]
        public int BrandId { get; set; }

        public string LogoImage { get; set; }

        public Department Department { get; set; }
        public Brand Brand { get; set; }

    }
}