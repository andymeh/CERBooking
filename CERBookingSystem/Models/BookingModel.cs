using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CERBookingSystemDAL;
using System.ComponentModel.DataAnnotations;

namespace CERBookingSystem.Models
{
    public class BookingModel
    {
        public string BookingId { get; set; }
        public User user { get; set; }
        public TrainRoute trainrouteRoute { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public DateTime dateOutbound { get; set; }
        public DateTime dateReturn { get; set; }
        public bool firstClass { get; set; }
        public int numberOfPassengers { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }

    public class NewBookingModel
    {
        public int selectedOutbound { get; set; }
        public int selectedReturn { get; set; }
        [Display(Name = "From")]
        [Required(ErrorMessage = "This field is required")]
        public int sourceCityId { get; set; }
        [Display(Name = "To")]
        [Required(ErrorMessage = "This field is required")]
        public int destinationCityId { get; set; }
        [Display(Name = "Date Outbound")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateOutbound { get; set; }
        [Display(Name = "Date Return")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dateReturn { get; set; }
        public bool firstClass { get; set; }
        [Display(Name = "Number Of Passengers")]
        [Required(ErrorMessage = "This field is required")]
        public int numberOfPassengers { get; set; }

    }
}