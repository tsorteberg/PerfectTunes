/***************************************************************
* Name        : InstrumentsGridBuilder.cs
* Author      : Tom Sorteberg
* Created     : 04/19/2021
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

namespace PerfectTunes.Models
{
    public class InstrumentsGridBuilder : GridBuilder
    {
        public InstrumentsGridBuilder(ISession sess) : base(sess) { }

        public InstrumentsGridBuilder(ISession sess, InstrumentsGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Department.IndexOf(FilterPrefix.Department) == -1;
            routes.BrandFilter = (isInitial) ? FilterPrefix.Brand + values.Brand : values.Brand;
            routes.DepartmentFilter = (isInitial) ? FilterPrefix.Department + values.Department : values.Department;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
        }

        public void LoadFilterSegments(string[] filter, Brand brand)
        {
            if (brand == null)
            {
                routes.BrandFilter = FilterPrefix.Brand + filter[0];
            }
            else
            {
                routes.BrandFilter = FilterPrefix.Brand + filter[0]
                    + "-" + brand.BrandName.Slug();
            }
            routes.DepartmentFilter = FilterPrefix.Department + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        string def = InstrumentsGridDTO.DefaultFilter;
        public bool IsFilterByBrand => routes.BrandFilter != def;
        public bool IsFilterByDepartment => routes.DepartmentFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsSortByDepartment =>
            routes.SortField.EqualsNoCase(nameof(Department));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Instrument.Price));
    }
}
