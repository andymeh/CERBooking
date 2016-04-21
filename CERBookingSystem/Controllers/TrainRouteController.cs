using System;
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
        /// <summary>
        /// Retrive AddNewTrain Form View
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewTrain()
        {
            return View();
        }
        /// <summary>
        /// Retrieve AddNewCity View
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewCity()
        {
            return View();
        }
        /// <summary>
        /// Retrieve AddNewRoute View
        /// Initialise model
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewRoute()
        {
            var model = new newRoute();
            
            model.cityDetails = getAllCityDetails();
            return View(model);
        }
        /// <summary>
        /// Retrieve AddNewTrainRoute View
        /// Initialise model
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewTrainRoute()
        {
            var model = new newTrainRoute();
            
            model.routeDetails = getAllRouteDetail();
            
            model.trainDetails = getAllTrainDetail();
            return View(model);
        }
        /// <summary>
        /// Get a list of train detail Models
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>List of route detail model</returns>
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
        /// <summary>
        /// Adds the new train to the database
        /// </summary>
        /// <param name="train"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Checks if all input data is valid
        /// Adds the new route to the database
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Verifies all the input information and adds the new train route to the database
        /// </summary>
        /// <param name="_trainRoute"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Verifies all the input information and adds the new city to the database
        /// </summary>
        /// <param name="_trainRoute"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Retrieves all of the cities from the database
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