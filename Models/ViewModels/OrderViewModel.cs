/***************************************************************
* Name        : PerfectTunes/Models/ViewModels/OrderViewModel.cs
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PerfectTunes.Models
{
    public class OrderViewModel
    {
        public CartViewModel Cart { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [StringLength(255)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter an address.")]
        [StringLength(255)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a city.")]
        [StringLength(255)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state.")]
        [StringLength(255)]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter a zip code.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid card number.")]
        [StringLength(255)]
        public string CCName { get; set; }

        [Required(ErrorMessage = "Please enter a credit card number.")]
        public string CCNum { get; set; }

        [Required(ErrorMessage = "Please enter month.  ")]
        public string CCMon { get; set; }

        [Required(ErrorMessage = "Please enter year.  ")]
        public string CCYear { get; set; }

        [Required(ErrorMessage = "Please enter the CVV code.")]
        public string CCCode { get; set; }
    }
}
