/***************************************************************
* Name        : PerfectTunes/Models/DomainModels/Order.cs
* Author      : Tom Sorteberg
* Created     : 05/03/2021
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectTunes.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        //[InverseProperty("Order")]
        public virtual List<OrderItem> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CCName { get; set; }
        [Required]
        public string CCNum { get; set; }
        [Required]
        public string CCMon { get; set; }
        [Required]
        public string CCYear { get; set; }
        [Required]
        public string CCCode { get; set; }
        [Required]
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }
    }
}
