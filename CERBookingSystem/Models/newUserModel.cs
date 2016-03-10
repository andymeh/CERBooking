﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystem.Models
{
    public class newUserModel
    {
        [Display(Name = "Username*")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, ErrorMessage = "Username can be no larger than 50 characters")]
        public string UserName { get; set; }

        [Display(Name = "Forename*")]
        [Required(ErrorMessage = "Forename is required")]
        [StringLength(50, ErrorMessage = "Forename can be no larger than 50 characters")]
        public string Forename { get; set; }

        [Display(Name = "Surname*")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname can be no larger than 50 characters")]
        public string Surname { get; set; }

        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password can be no longer than 40 characters")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password*")]
        [Required(ErrorMessage = "Password Confirmation is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password can be no longer than 40 characters")]
        public string Password2 { get; set; }

        [Display(Name = "Email*")]
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail { get; set; }
    }
}