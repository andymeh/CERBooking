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
        // GET: TrainRoute
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddNewTrain()
        {
            return View();
        }
        public ActionResult AddNewRoute()
        {
            var model = new newRoute();
            List<cityDetails> allCitiesDetail = new List<cityDetails>();

            List<City> allCities = CitiesBLL.getAllCities();
            foreach(var c in allCities)
            {
                allCitiesDetail.Add(new cityDetails
                {
                    cityId = c.CityId,
                    cityName = c.CityName
                });
            }
            model.cityDetails = allCitiesDetail;
            return View(model);
        }
        public ActionResult AddNewTrainRoute()
        {
            var model = new newTrainRoute();

            List<Route> allRoutes = RouteBLL.getAllRoutes();
            List<RouteDetail> allRouteDetail = new List<RouteDetail>();
            foreach(var r in allRoutes)
            {
                allRouteDetail.Add(new RouteDetail
                {
                    routeId = r.RouteId,
                    sourceCityName = CitiesBLL.getCity(r.Source).CityName,
                    destCityName = CitiesBLL.getCity(r.Destination).CityName,
                    depatTime = r.DepartureTime,
                    arrivalTime = r.ArrivalTime
                });
            }
            model.routeDetails = allRouteDetail;

            List<Train> allTrains = TrainBLL.getAllTrains();
            List<TrainDetail> allTrainDetail = new List<TrainDetail>();
            foreach(var t in allTrains)
            {
                allTrainDetail.Add(new TrainDetail
                {
                    trainId = t.TrainId,
                    firstClassCapacity = t.CapacityFirst,
                    econClassCapacity = t.CapacityEconomy
                });
            }
            model.trainDetails = allTrainDetail;
            return View(model);
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
            return View(route);
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
                    EconomySeats = train.CapacityEconomy
                };
                TrainRouteBLL.addTrainRoute(newTrainRoute, _trainRoute.startDate, _trainRoute.endDate);
                return RedirectToAction("Index", "Home");
            }
            return View(_trainRoute);
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