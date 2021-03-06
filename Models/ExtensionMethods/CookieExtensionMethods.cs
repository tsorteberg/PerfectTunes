/***************************************************************
* Name        : CookieExtensionMethod.cs
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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace PerfectTunes.Models
{
    // extension methods that make working with request and response cookies 
    // similar to working with session

    public static class CookieExtensionMethods
    {
        public static string GetString(this IRequestCookieCollection cookies, string key) =>
            cookies[key];

        public static int? GetInt32(this IRequestCookieCollection cookies, string key) =>
            int.TryParse(cookies[key], out int i) ? i : (int?)null;

        public static T GetObject<T>(this IRequestCookieCollection cookies, string key)
        {
            var value = cookies[key];
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }


        public static void SetString(this IResponseCookies cookies, string key,
            string value, int days = 30)
        {
            cookies.Delete(key); // delete old value first
            if (days == 0)
            {
                cookies.Append(key, value);
            }
            else
            {
                CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(days) };
                cookies.Append(key, value, options);
            }

        }
        public static void SetInt32(this IResponseCookies cookies, string key,
            int value, int days = 30) =>
            cookies.SetString(key, value.ToString(), days);

        public static void SetObject<T>(this IResponseCookies cookies, string key,
            T value, int days = 30) =>
            cookies.SetString(key, JsonConvert.SerializeObject(value), days);

    }
}