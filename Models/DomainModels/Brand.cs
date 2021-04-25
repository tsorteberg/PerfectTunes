/***************************************************************
* Name        : Brand.cs
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
using System.ComponentModel.DataAnnotations;

namespace PerfectTunes.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please enter a brand name.")]
        [MaxLength(200)]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Please enter a product line.")]
        [MaxLength(200)]
        public string ProductLine { get; set; }

        public string LogoImage { get; set; }
    }
}