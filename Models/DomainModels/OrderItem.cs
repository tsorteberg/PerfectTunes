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
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectTunes.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //[ForeignKey("OrderId")]
        //[InverseProperty("OrderItems")]
        public virtual Order Order { get; set; }
       
    }
}
