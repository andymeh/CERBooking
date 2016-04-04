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
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewBooking(SearchModel newBooking)
        {
            if (Request.IsAuthenticated)
            {
                NewBookingModel nBooking = new NewBookingModel();
                nBooking.isReturn = false;
                TrainRoute outTrainRoute = TrainRouteBLL.GetTrainRoute(newBooking.selectedOutbound);
                Route outRoute = RouteBLL.getRoute(outTrainRoute.RouteId);
                nBooking.numberOfPassengers = newBooking.bookingDetails.numberOfPassengers;
                User user = UserBLL.getUser(User.Identity.Name);
                nBooking.usersDetails = new userDetail{userId = user.UserId, forename = user.Forename, surname = user.Surname, emailAddress = user.EmailAddress };
                nBooking.selectedOutbound = new SearchTrainRoute
                {
                    TrainRouteId = outTrainRoute.TrainRouteId,
                    Source = CitiesBLL.getCity(outRoute.Source).CityName,
                    Destination = CitiesBLL.getCity(outRoute.Destination).CityName,
                    departureTime = outRoute.DepartureTime,
                    arrivalTime = outRoute.ArrivalTime
                };
                if (newBooking.selectedReturn != 0)
                {
                    nBooking.isReturn = true;
                    TrainRoute retTrainRoute = TrainRouteBLL.GetTrainRoute(newBooking.selectedReturn);
                    Route retRoute = RouteBLL.getRoute(retTrainRoute.RouteId);
                    nBooking.selectedReturn = new SearchTrainRoute
                    {
                        TrainRouteId = retTrainRoute.TrainRouteId,
                        Source = CitiesBLL.getCity(retRoute.Source).CityName,
                        Destination = CitiesBLL.getCity(retRoute.Destination).CityName,
                        departureTime = retRoute.DepartureTime,
                        arrivalTime = retRoute.ArrivalTime
                    };
                }

                return View(nBooking);
            }
            return View(Url.RequestContext.ToString(), newBooking);
        }

        [HttpPost]
        public ActionResult ConfirmBooking(NewBookingModel bookingDetails)
        {
            Booking dalBookingOutBound = new Booking
            {
                TrainRouteId = bookingDetails.selectedOutbound.TrainRouteId,
                UserId = bookingDetails.usersDetails.userId,
                NoInParty = bookingDetails.numberOfPassengers,
                statusOfBooking = "Active",
                DateBooked = DateTime.Now
            };
            BookingBLL.
            if (bookingDetails.isReturn)
            {
                Booking dalBookingReturn = new Booking
                {
                    TrainRouteId = bookingDetails.selectedReturn.TrainRouteId,
                    UserId = bookingDetails.usersDetails.userId,
                    NoInParty = bookingDetails.numberOfPassengers,
                    statusOfBooking = "Active",
                    DateBooked = DateTime.Now
                };
            }
        }
    }
}