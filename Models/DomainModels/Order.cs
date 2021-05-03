using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectTunes.Models.DomainModels
{
    public class Order
    {
        public int OrderId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        [InverseProperty("Order")]
        public ICollection<OrderItem> OrderItems { get; set; }
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
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }
    }
}
