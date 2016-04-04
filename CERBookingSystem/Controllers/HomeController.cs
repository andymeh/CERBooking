using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystemDAL;
using CERBookingSystemBLL;
using CERBookingSystem.Models;
using System.Web.Security;

namespace CERBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new SearchModel();
            model.cityDetails = getAllCityDetails();
            return View(model);
        }
        public PartialViewResult Login()
        {
            var model = new UserLogin();
            return PartialView("Login", model);
        }
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                if (UserBLL.isUserValid(user.emailAddress, user.password))
                {
                    FormsAuthentication.SetAuthCookie(user.emailAddress, user.rememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        [HttpGet]
        [Route("GetCities")]
        public JsonResult GetCities()
        {
            List<string> cityNameList = new List<string>();
            List<City> cities = CitiesBLL.getAllCities();
            foreach(var city in cities)
            {
                cityNameList.Add(city.CityName);
            }
            return Json(cityNameList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Search for train routes from the home page
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public ActionResult Search(SearchModel searchTerms)
        {
            //
            if (ModelState.IsValid)
            {
                SearchModel searchDetails = new SearchModel();
                List<TrainRoute> outboundTrainRoutes = new List<TrainRoute>();
                List<TrainRoute> returnTrainRoutes = new List<TrainRoute>();

                searchDetails.OutboundTrainRoutes = new List<SearchTrainRoute>();
                searchDetails.ReturnTrainRoutes = new List<SearchTrainRoute>();

                searchDetails.bookingDetails = searchTerms.bookingDetails;

                City sourceCity = CitiesBLL.getCity(searchTerms.bookingDetails.sourceCityId);
                searchDetails.sourceCity = sourceCity.CityName;

                City destnationCity = CitiesBLL.getCity(searchTerms.bookingDetails.destinationCityId);
                searchDetails.destCity = destnationCity.CityName;
                if(sourceCity != null && destnationCity != null)
                {
                    List<Route> OutboundRoutes = RouteBLL.getRoutes(sourceCity, destnationCity);
                    
                    foreach (var route in OutboundRoutes)
                    {
                        outboundTrainRoutes.AddRange(TrainRouteBLL.GetTrainRoutes(route.RouteId, searchTerms.bookingDetails.dateOutbound));
                    }

                    foreach (var tr in outboundTrainRoutes)
                    {
                        Route route = RouteBLL.getRoute(tr.RouteId);
                        searchDetails.OutboundTrainRoutes.Add(new SearchTrainRoute
                        {
                            TrainRouteId = tr.TrainRouteId,
                            Source = sourceCity.CityName,
                            Destination = destnationCity.CityName,
                            departureTime = route.DepartureTime,
                            arrivalTime = route.ArrivalTime,
                            firstSeats = tr.FirstClassSeats,
                            econSeats = tr.EconomySeats
                        });
                    }

                    if (searchTerms.bookingDetails.dateReturn != null && searchTerms.bookingDetails.dateReturn != DateTime.MinValue)
                    {
                        List<Route> ReturnRoutes = RouteBLL.getRoutes(destnationCity, sourceCity);
                        foreach (var route in ReturnRoutes)
                        {
                            returnTrainRoutes.AddRange(TrainRouteBLL.GetTrainRoutes(route.RouteId, searchTerms.bookingDetails.dateReturn));
                            
                        }
                        foreach (var tr in returnTrainRoutes)
                        {
                            Route route = RouteBLL.getRoute(tr.RouteId);
                            searchDetails.ReturnTrainRoutes.Add(new SearchTrainRoute
                            {
                                TrainRouteId = tr.TrainRouteId,
                                Source = destnationCity.CityName,
                                Destination = sourceCity.CityName,
                                departureTime = route.DepartureTime,
                                arrivalTime = route.ArrivalTime,
                                firstSeats = tr.FirstClassSeats,
                                econSeats = tr.EconomySeats
                            });
                        }
                    }
                    return View("Search", searchDetails);
                }
                else if(sourceCity == null)
                {
                   ModelState.AddModelError("", "Username already exists");
                }
            }
            searchTerms.cityDetails = getAllCityDetails();
            return View("Index", searchTerms);
        }
        public List<cityDetails> getAllCityDetails()
        {
            List<cityDetails> allCitiesDetail = new List<cityDetails>();

            List<City> allCities = CitiesBLL.getAllCities();
            foreach (var c in allCities)
            {
                allCitiesDetail.Add(new cityDetails
                {
                    cityId = c.CityId,
                    cityName = c.CityName
                });
            }
            return allCitiesDetail;
        }
    }
}