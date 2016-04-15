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
        public SearchTrainRoute selectedOutbound { get; set; }
        public SearchTrainRoute selectedReturn { get; set; }
        public userDetail usersDetails { get; set; }
        [Display(Name = "Outbound Date")]
        [Required(ErrorMessage = "Class is required")]
        public DateTime dateOutbound { get; set; }
        [Display(Name = "Return Date")]
        public DateTime? dateReturn { get; set; }
        [Display(Name = "Class")]
        [Required(ErrorMessage = "Class is required")]
        public bool firstClass { get; set; }
        [Display(Name = "Number of Passengers")]
        [Required(ErrorMessage = "Number of Passengers is required")]
        public int numberOfPassengers { get; set; }
        [Display(Name = "From")]
        [Required(ErrorMessage = "Source City is required")]
        public int sourceCityId { get; set; }
        [Display(Name = "To")]
        [Required(ErrorMessage = "Destination City is required")]
        public int destinationCityId { get; set; }
        public bool isReturn { get; set; }
        public double price { get; set; }
    }
}