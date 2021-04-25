/***************************************************************
* Name        : RouteDictionary.cs
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerfectTunes.Models
{

    public static class FilterPrefix
    {
        public const string Department = "department-";
        public const string Price = "price-";
        public const string Brand = "brand-";
    }

    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) &&
                current.SortDirection == "asc")
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
        }

        public string DepartmentFilter
        {
            get => Get(nameof(InstrumentsGridDTO.Department))?.Replace(FilterPrefix.Department, "");
            set => this[nameof(InstrumentsGridDTO.Department)] = value;
        }

        public string PriceFilter
        {
            get => Get(nameof(InstrumentsGridDTO.Price))?.Replace(FilterPrefix.Price, "");
            set => this[nameof(InstrumentsGridDTO.Price)] = value;
        }

        public string BrandFilter
        {
            get
            {
                string s = Get(nameof(InstrumentsGridDTO.Brand))?.Replace(FilterPrefix.Brand, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(InstrumentsGridDTO.Brand)] = value;
        }

        public void ClearFilters()
        {
            DepartmentFilter = FilterPrefix.Department + InstrumentsGridDTO.DefaultFilter;
            PriceFilter = FilterPrefix.Price + InstrumentsGridDTO.DefaultFilter;
            BrandFilter = FilterPrefix.Brand + InstrumentsGridDTO.DefaultFilter;
        }

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }
    }
}
