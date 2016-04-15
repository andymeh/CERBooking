﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystem.Models;
using CERBookingSystemDAL;
using CERBookingSystemBLL;

namespace CERBookingSystem.Controllers
{
    public class TrainRouteController : Controller
    {
        public ActionResult AddNewTrain()
        {
            return View();
        }
        public ActionResult AddNewCity()
        {
            return View();
        }
        public ActionResult AddNewRoute()
        {
            var model = new newRoute();
            
            model.cityDetails = getAllCityDetails();
            return View(model);
        }
        public ActionResult AddNewTrainRoute()
        {
            var model = new newTrainRoute();
            
            model.routeDetails = getAllRouteDetail();
            
            model.trainDetails = getAllTrainDetail();
            return View(model);
        }

        private List<TrainDetail> getAllTrainDetail()
        {
            List<Train> allTrains = TrainBLL.getAllTrains();
            List<TrainDetail> allTrainDetail = new List<TrainDetail>();
            foreach (var t in allTrains)
            {
                allTrainDetail.Add(new TrainDetail
                {
                    trainId = t.TrainId,
                    firstClassCapacity = t.CapacityFirst,
                    econClassCapacity = t.CapacityEconomy
                });
            }
            return allTrainDetail;
        }

        private List<RouteDetail> getAllRouteDetail()
        {
            List<Route> allRoutes = RouteBLL.getAllRoutes();
            List<RouteDetail> allRouteDetail = new List<RouteDetail>();
            foreach (var r in allRoutes)
            {
                allRouteDetail.Add(new RouteDetail
                {
                    routeId = r.RouteId,
                    sourceCityName = CitiesBLL.getCity(r.Source).CityName,
                    destCityName = CitiesBLL.getCity(r.Destination).CityName,
                    departTime = r.DepartureTime,
                    arrivalTime = r.ArrivalTime
                });
            }
            return allRouteDetail.OrderBy(x => x.sourceCityName).ThenBy(x => x.destCityName).ThenBy(x => x.departTime).ThenBy(x => x.arrivalTime).ToList();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddNewTrain(newTrain train)
        {
            if (ModelState.IsValid)
            {
                Train dalTrain = new Train
                {
                    CapacityFirst = train.firstClassCapacity,
                    CapacityEconomy = train.secondClassCapacity
                };
                TrainBLL.addTrain(dalTrain);
                ViewData["Message"] = "Success";
            }
            return View(train);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddNewRoute(newRoute route)
        {
            if (ModelState.IsValid)
            {
                Route dalRoute = new Route
                {
                    Source = route.sourceCityId,
                    Destination = route.destinationCityId,
                    DepartureTime = route.departureTime,
                    ArrivalTime = route.arrivalTime,
                    Distance = route.distance
                };
                RouteBLL.addRoute(dalRoute);
            }
            var model = new newRoute();
            model.cityDetails = getAllCityDetails();
            ViewData["Message"] = "Success";
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddNewTrainRoute(newTrainRoute _trainRoute)
        {
            if (ModelState.IsValid)
            {
                Train train = TrainBLL.getTrain(_trainRoute.TrainId);
                TrainRoute newTrainRoute = new TrainRoute
                {
                    TrainId = _trainRoute.TrainId,
                    RouteId = _trainRoute.RouteId,
                    FirstClassSeats = train.CapacityFirst,
                    EconomySeats = train.CapacityEconomy,
                    CostFirstClass = _trainRoute.CostFirstClass,
                    CostEconomy = _trainRoute.CostEconomyClass
                };
                TrainRouteBLL.addTrainRoute(newTrainRoute, _trainRoute.startDate, _trainRoute.endDate);
            }

            var model = new newTrainRoute();
            model.routeDetails = getAllRouteDetail();
            model.trainDetails = getAllTrainDetail();
            ViewData["Message"] = "Success";
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddNewCity(newCity _city)
        {
            if (ModelState.IsValid)
            {
                City city = CitiesBLL.getCity(_city.cityName);
                if (city == null)
                {
                    City dalCity = new City { CityName = _city.cityName };
                    CitiesBLL.addCity(dalCity);
                    ViewData["Message"] = "Success";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", city.CityName + " is already listed.");
                }
            }
            return View(_city);
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