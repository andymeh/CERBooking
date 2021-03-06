﻿using System;
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
        /// <summary>
        /// Controller to intialise the home model and
        ///  return the Home Index view
        /// </summary>
        /// <returns>View : Home/Index</returns>
        public ActionResult Index()
        {
            var model = new SearchModel();
            model.cityDetails = getAllCityDetails();
            return View(model);
        }
        /// <summary>
        /// Controller to return the Contact Us view
        /// </summary>
        /// <returns>View : Home/ContactUs</returns>
        public ActionResult ContactUs()
        {
            return View();
        }
        /// <summary>
        /// Retrieves the Login Partial view & Intialises the UserLogin Model
        /// </summary>
        /// <returns>PartialView : Home/Login</returns>
        public PartialViewResult Login()
        {
            var model = new UserLogin();
            return PartialView("Login", model);
        }
        /// <summary>
        /// Controller to enable the user to login save their search details
        /// </summary>
        /// <param name="searchDetails">Search Model with users search details</param>
        /// <returns>Partial View : Home/SearchLogin</returns>
        public PartialViewResult SearchLogin(SearchModel searchDetails)
        {
            SearchUserLogin model = new SearchUserLogin();
            model.loginInfo = new UserLogin();
            model.searchModel = searchDetails;
            return PartialView("SearchLogin", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                if (UserBLL.isUserValid(user.emailAddress, user.password))
                {
                    FormsAuthentication.SetAuthCookie(user.emailAddress, user.rememberMe);
                    return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult SearchLogin(SearchUserLogin searchLoginDetails)
        {
            if (ModelState.IsValid)
            {
                if (UserBLL.isUserValid(searchLoginDetails.loginInfo.emailAddress, searchLoginDetails.loginInfo.password))
                {
                    FormsAuthentication.SetAuthCookie(searchLoginDetails.loginInfo.emailAddress, searchLoginDetails.loginInfo.rememberMe);
                    var searchView = Search(searchLoginDetails.searchModel);
                    return searchView;
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(searchLoginDetails.loginInfo);
        }
        [HttpPost]
        public JsonResult GetCities(int districtId)
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

                //initialise and fill the Sea
                searchDetails.bookingDetails = new NewBookingModel();
                searchDetails.cityDetails = getAllCityDetails();
                searchDetails.OutboundTrainRoutes = new List<SearchTrainRoute>();
                searchDetails.ReturnTrainRoutes = new List<SearchTrainRoute>();
                searchDetails.bookingDetails.isReturn = false;

                searchDetails.bookingDetails = searchTerms.bookingDetails;

                //get city from the database
                City sourceCity = CitiesBLL.getCity(searchTerms.bookingDetails.sourceCityId);
                searchDetails.sourceCity = sourceCity.CityName;
                searchDetails.bookingDetails.numberOfPassengers = searchTerms.bookingDetails.numberOfPassengers;

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
                            econSeats = tr.EconomySeats,
                            costEconClass = tr.CostEconomy,
                            costFirstClass = tr.CostFirstClass
                            
                        });
                    }

                    if (searchTerms.bookingDetails.dateReturn != null && searchTerms.bookingDetails.dateReturn != DateTime.MinValue)
                    {
                        searchDetails.bookingDetails.isReturn = true;
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
                                econSeats = tr.EconomySeats,
                                costEconClass = tr.CostEconomy,
                                costFirstClass = tr.CostFirstClass
                            });
                        }
                    }
                    //ensure that trainroutes are available for the users search
                    if(searchDetails.OutboundTrainRoutes.Count > 0)
                    {
                        if (!searchDetails.bookingDetails.isReturn)
                        {
                            return View("Search", searchDetails);
                        }
                        else if (searchDetails.ReturnTrainRoutes.Count > 0)
                        {
                            return View("Search", searchDetails);
                        }
                        else
                        {
                            ViewData["Message"] = "Success";
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "Success";
                    }
                }
                else if(sourceCity == null)
                {
                   ModelState.AddModelError("", "Please select a source city.");
                }
            }
            searchTerms.cityDetails = getAllCityDetails();
            return View("Index", searchTerms);
        }
        /// <summary>
        /// Retrieve all the cities in the database
        /// </summary>
        /// <returns></returns>
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