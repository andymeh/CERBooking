using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CERBookingSystem.Models
{

    public class newTrain
    {
        [Display(Name = "First Class Capacity*")]
        [Required(ErrorMessage = "Please enter a capacity for first class")]
        public int firstClassCapacity { get; set; }
        [Display(Name = "Second Class Capacity*")]
        [Required(ErrorMessage = "Please enter a capacity for Second class")]
        public int secondClassCapacity { get; set; }
    }

    public class newCity
    {
        [Display(Name = "City Name*")]
        [Required(ErrorMessage = "A city name is required")]
        public string cityName { get; set; }
    }

    public class newRoute
    {
        
        public List<cityDetails> cityDetails { get; set; }
        public IEnumerable<SelectListItem> CityListItems
        {
            get
            {
                var allCities = cityDetails.Select(f => new SelectListItem
                {
                    Value = f.cityId.ToString(),
                    Text = f.cityName
                });
                return allCities;

            }
        }
       
        [Display(Name = "Source City*")]
        [Required(ErrorMessage = "Please select a city")]
        public int sourceCityId { get; set; }
        [Display(Name = "Destination City*")]
        [Required(ErrorMessage = "Please select a city")]
        public int destinationCityId { get; set; }
        [Display(Name = "Distance*")]
        [Required(ErrorMessage = "Please enter a distance")]
        public int distance { get; set; }
        [Display(Name = "Departure Time*")]
        [Required(ErrorMessage = "Please enter a departure time")]
        public TimeSpan departureTime { get; set; }
        [Display(Name = "Arrival Time*")]
        [Required(ErrorMessage = "Please enter an arrival time")]
        public TimeSpan arrivalTime { get; set; }
    }

    public class newTrainRoute
    {
        public List<TrainDetail> trainDetails { get; set; }
        public IEnumerable<SelectListItem> TrainListItems
        {
            get
            {
                var allTrains = trainDetails.Select(f => new SelectListItem
                {
                    Value = f.trainId.ToString(),
                    Text = "1st : " + f.firstClassCapacity.ToString() + " | 2nd : " + f.econClassCapacity.ToString()
                });
                return allTrains;

            }
        }

        public List<RouteDetail> routeDetails { get; set; }
        public IEnumerable<SelectListItem> RouteItems
        {
            get
            {
                var allRoutes = routeDetails.Select(f => new SelectListItem
                {
                    Value = f.routeId.ToString(),
                    Text = f.sourceCityName + " @ " + f.departTime.ToString("") + " ~ " + f.destCityName + " @ " + f.arrivalTime.ToString("")
                });
                return allRoutes;

            }
        }

        [Display(Name = "Train*")]
        [Required(ErrorMessage = "Please select a train!")]
        public int TrainId { get; set; }
        [Display(Name = "Route*")]
        [Required(ErrorMessage = "Please select a route!")]
        public int RouteId { get; set; }

        private DateTime _startDate = DateTime.MinValue;
        [Display(Name = "Start Date*")]
        [Required(ErrorMessage = "Please select a start date!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime startDate
        {
            get
            {
                return (_startDate == DateTime.MinValue) ? DateTime.Now : _startDate;
            }
            set { _startDate = value; }
        }

        private DateTime _endDate = DateTime.MinValue;

        [Display(Name = "End Date*")]
        [Required(ErrorMessage = "Please select an end date!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime endDate
        {
            get
            {
                return (_endDate == DateTime.MinValue) ? DateTime.Now : _endDate;
            }
            set { _endDate = value; }
        }

        [Display(Name = "Cost of First Class Ticket")]
        [Required(ErrorMessage = "Please enter the price for a first class ticket!")]
        public double CostFirstClass { get; set; }

        [Display(Name = "Cost of Economy Class Ticket")]
        [Required(ErrorMessage = "Please enter the price for an economy class ticket!")]
        public double CostEconomyClass { get; set; }
    }

    public class TrainDetail
    {
        public int trainId { get; set; }
        public int firstClassCapacity { get; set; }
        public int econClassCapacity { get; set; }
    }

    public class RouteDetail
    {
        public int routeId { get; set; }
        public string sourceCityName { get; set; }
        public string destCityName { get; set; }
        public TimeSpan departTime { get; set; }
        public TimeSpan arrivalTime { get; set; }
    }
    public class cityDetails
    {
        public int cityId { get; set; }
        public string cityName { get; set; }
    }
    public class SearchTrainRoute
    {
        public int TrainRouteId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public TimeSpan departureTime { get; set; }
        public TimeSpan arrivalTime { get; set; }
        public int firstSeats { get; set; }
        public int econSeats { get; set; }
    }
}