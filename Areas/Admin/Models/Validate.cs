﻿/***************************************************************
* Name        : Admin/Models/Validate.cs
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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PerfectTunes.Models
{
    // used by client-side and server-side remote validation checks
    public class Validate
    {
        // private constants for working with TempData
        private const string DepartmentKey = "validDepartment";
        private const string BrandKey = "validBrand";

        // constructor and private TempData property
        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        // public properties
        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        // genre
        public void CheckDepartment(string departmentId, Repository<Department> data)
        {
            Department entity = data.Get(departmentId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Department id {departmentId} is already in the database.";
        }
        public void MarkDepartmentChecked() => tempData[DepartmentKey] = true;
        public void ClearDepartment() => tempData.Remove(DepartmentKey);
        public bool IsDepartmentChecked => tempData.Keys.Contains(DepartmentKey);

        // author
        public void CheckBrand(string brandName, string productLine, string operation, Repository<Brand> data)
        {
            Brand entity = null;
            if (Operation.IsAdd(operation)) // only check database on add
            {
                entity = data.Get(new QueryOptions<Brand>
                {
                    Where = b => b.BrandName == brandName && b.ProductLine == productLine
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Brand {entity.BrandName} is already in the database.";
        }
        public void MarkBrandChecked() => tempData[BrandKey] = true;
        public void ClearBrand() => tempData.Remove(BrandKey);
        public bool IsBrandChecked => tempData.Keys.Contains(BrandKey);
    }
}