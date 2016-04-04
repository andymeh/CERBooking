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
        public DateTime dateOutbound { get; set; }
        public DateTime? dateReturn { get; set; }
        public bool firstClass { get; set; }
        public int numberOfPassengers { get; set; }
        public int sourceCityId { get; set; }
        public int destinationCityId { get; set; }
        public bool isReturn { get; set; }
        public double price { get; set; }
    }
}