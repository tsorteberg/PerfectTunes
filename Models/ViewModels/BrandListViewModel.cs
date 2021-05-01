﻿/***************************************************************
* Name        : PerfectTunes/Models/ViewModels/BrandListViewModel.cs
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
using System.Collections.Generic;

namespace PerfectTunes.Models
{
    public class BrandListViewModel
    {
        public IEnumerable<Brand> Brands { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}