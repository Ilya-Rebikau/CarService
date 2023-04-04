﻿using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Account
{
    public class AccountViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}