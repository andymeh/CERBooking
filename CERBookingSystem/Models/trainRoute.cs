using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CERBookingSystem.Models
{
    
    public class newTrain
    {
        public int firstClassCapacity { get; set; }
        public int secondClassCapacity { get; set; }
    }

    public class newRoute
    {
        public int source { get; set; }
        public int destination { get; set; }
        public int distance { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
    }
}