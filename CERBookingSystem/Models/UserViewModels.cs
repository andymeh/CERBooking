
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

        [Display(Name = "Confirm Email*")]
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserEmail2 { get; set; }
    }

    public class UserLogin
    {
        [Display(Name = "Email Address*")]
        [Required(ErrorMessage = "Please enter a Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string emailAddress { get; set; }
        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Please enter a Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Rememeber me")]
        public bool rememberMe { get; set; }
    }
    public class SearchUserLogin
    {
        public UserLogin loginInfo {get; set;}
        public SearchModel searchModel { get; set; }
    }

    public class userDetail
    {
        public int userId { get; set; }
        public string forename { get; set; }
        public string surname { get; set; }
        public string emailAddress { get; set; }
    }
}