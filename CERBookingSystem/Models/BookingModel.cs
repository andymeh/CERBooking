using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERBookingSystem.Models
{
    public class BookingModel
    {
        public string depStation { get; set; }
        public string endStation { get; set; }
        public DateTime dateOfTravel { get; set; }
    }
}