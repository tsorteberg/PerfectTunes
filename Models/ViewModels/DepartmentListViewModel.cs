/***************************************************************
* Name        : DepartmentListViewModel.cs
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
using System.Collections.Generic;

namespace PerfectTunes.Models
{
    public class DepartmentListViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
