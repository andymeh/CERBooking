using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CERBookingSystem.Models
{
    public class SearchModel
    {
        public List<SearchTrainRoute> OutboundTrainRoutes { get; set; }
        public List<SearchTrainRoute> ReturnTrainRoutes { get; set; }
        public NewBookingModel bookingDetails { get; set; }
        [Display(Name = "Source City*")]
        [Required(ErrorMessage = "Please enter a city")]
        public string sourceCity { get; set; }
        [Display(Name = "Destination City*")]
        [Required(ErrorMessage = "Please enter a city")]
        public string destCity { get; set; }
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
    }
    
}