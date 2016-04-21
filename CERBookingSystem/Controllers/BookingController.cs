using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystem.Models;
using CERBookingSystemDAL;
using CERBookingSystemBLL;
using System.Net.Mail;
using System.Net;

namespace CERBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// If the user tries to access this view when there is no 
        /// model then the will be redirected to the home page 
        /// </summary>
        /// <returns></returns>
        public ActionResult NewBooking()
        {
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Retrieves the User Bookings view
        /// </summary>
        /// <returns>View : Booking/UserBookings</returns>
        public ActionResult UserBookings()
        {
            var model = new List<BookingModel>();
            User user = UserBLL.getUser(User.Identity.Name);
            List<Booking> userBookings = BookingBLL.getAllBookingsForUser(user.UserId);
            
            //Loop through each booking made by the user to convert tho BookingModel
            foreach(var b in userBookings)
            {
                TrainRoute tr = TrainRouteBLL.GetTrainRoute(b.TrainRouteId);
                Route r = RouteBLL.getRoute(tr.RouteId);
                model.Add(new BookingModel
                {
                    BookingId = b.BookingId.ToString(),
                    source = CitiesBLL.getCity(r.Source).CityName,
                    destination = CitiesBLL.getCity(r.Destination).CityName,
                    status = b.statusOfBooking,
                    dateOutbound = tr.Date,
                    dateReturn = tr.Date
                }); 
            }
            
            return View(model);
        }
        
        /// <summary>
        /// HttpPost
        /// Initialise the Booking view with the data from the train routes selected by the user
        /// </summary>
        /// <param name="newBooking">Class : SearchModel</param>
        /// <returns>View : Booking/NewBooking</returns>
        [HttpPost]
        public ActionResult NewBooking(SearchModel newBooking)
        {
            if (Request.IsAuthenticated)
            {
                //if(newBooking.selectedOutbound == 0 || newBooking.selectedReturn == 0)
                //{
                //    if(newBooking.selectedOutbound == 0)
                //    {
                //        ViewData["Message"] = "Error";
                //        return View("../Home/Search", newBooking);
                //    }
                //    else if(newBooking.bookingDetails.isReturn && newBooking.selectedReturn == 0)
                //    {
                //        ViewData["Message"] = "Error";
                //        return View("../Home/Search", newBooking);
                //    }
                //}
                NewBookingModel nBooking = new NewBookingModel();
                nBooking.isReturn = false;
                double totalPrice = 0;
                
                //Fill the NewBookingModel values
                TrainRoute outTrainRoute = TrainRouteBLL.GetTrainRoute(newBooking.selectedOutbound);
                Route outRoute = RouteBLL.getRoute(outTrainRoute.RouteId);
                nBooking.numberOfPassengers = newBooking.bookingDetails.numberOfPassengers;
                nBooking.dateOutbound = newBooking.bookingDetails.dateOutbound;
                nBooking.dateReturn = newBooking.bookingDetails.dateReturn;

                //get the user from the database using their email address
                User user = UserBLL.getUser(User.Identity.Name);
                nBooking.usersDetails = new userDetail{userId = user.UserId, forename = user.Forename, surname = user.Surname, emailAddress = user.EmailAddress };

                //Convert the selected trainroute to a SearchTrainRoute class
                nBooking.selectedOutbound = new SearchTrainRoute
                {
                    TrainRouteId = outTrainRoute.TrainRouteId,
                    Source = CitiesBLL.getCity(outRoute.Source).CityName,
                    Destination = CitiesBLL.getCity(outRoute.Destination).CityName,
                    departureTime = outRoute.DepartureTime,
                    arrivalTime = outRoute.ArrivalTime
                };

                if(newBooking.bookingDetails.firstClass == true)
                {
                    totalPrice += outTrainRoute.CostFirstClass * nBooking.numberOfPassengers;
                }
                else
                {
                    totalPrice += outTrainRoute.CostEconomy * newBooking.bookingDetails.numberOfPassengers;
                }

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

                    if (newBooking.bookingDetails.firstClass == true)
                    {
                        totalPrice += retTrainRoute.CostFirstClass * nBooking.numberOfPassengers;
                    }
                    else
                    {
                        totalPrice += retTrainRoute.CostEconomy * newBooking.bookingDetails.numberOfPassengers;
                    }
                }
                nBooking.price = totalPrice;
                
                return View(nBooking);
            }
            //return the user if the Model state is not valid
            return View(Url.RequestContext.ToString(), newBooking);
        }

        /// <summary>
        /// Add the booking to the database
        /// </summary>
        /// <param name="bookingDetails">NewBookingModel : Requires User Details and TrainRoute details</param>
        /// <returns>View : User/UserBookings</returns>
        [HttpPost]
        public ActionResult ConfirmBooking(NewBookingModel bookingDetails)
        {
            Booking dalBookingOutBound = new Booking
            {
                TrainRouteId = bookingDetails.selectedOutbound.TrainRouteId,
                UserId = bookingDetails.usersDetails.userId,
                NoInParty = bookingDetails.numberOfPassengers,
                statusOfBooking = "Active",
                DateBooked = DateTime.Now,
                FirstClass = bookingDetails.firstClass
            };
            //insert booking into datebase
            BookingBLL.addBooking(dalBookingOutBound);
            TrainRouteBLL.updateSeatNumber(bookingDetails.firstClass, bookingDetails.numberOfPassengers, bookingDetails.selectedOutbound.TrainRouteId);

            if (bookingDetails.isReturn)
            {

                Booking dalBookingReturn = new Booking
                {
                    TrainRouteId = bookingDetails.selectedReturn.TrainRouteId,
                    UserId = bookingDetails.usersDetails.userId,
                    NoInParty = bookingDetails.numberOfPassengers,
                    statusOfBooking = "Active",
                    DateBooked = DateTime.Now,
                    FirstClass = bookingDetails.firstClass
                };
                BookingBLL.addBooking(dalBookingReturn);
                TrainRouteBLL.updateSeatNumber(bookingDetails.firstClass, bookingDetails.numberOfPassengers, bookingDetails.selectedReturn.TrainRouteId);
            }
            //sendConfirmEmail(bookingDetails);
            return RedirectToAction("UserBookings","Booking");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public ActionResult CancelBooking(int bookingId)
        {
            Booking booking = BookingBLL.getBooking(bookingId);
            User user = UserBLL.getUser(User.Identity.Name);
            if(booking.UserId == user.UserId || user.Employee)
            {
                BookingBLL.cancelBooking(bookingId);
                int negNoInparty = 0 - booking.NoInParty;
                TrainRouteBLL.updateSeatNumber(booking.FirstClass, negNoInparty, booking.TrainRouteId);
            }
            return RedirectToAction("UserBookings", "Booking");
        }

        /// <summary>
        /// Send email confirmation to the user
        /// </summary>
        /// <param name="bookingDetails"></param>
        public void sendConfirmEmail(NewBookingModel bookingDetails)
        {
            User user = UserBLL.getUser(User.Identity.Name);
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.EmailAddress));  // replace with valid value 
            message.From = new MailAddress("cer@gmail.com");  // replace with valid value
            message.Subject = "China Express Railways Booking Confirmation.";
            message.Body = String.Format(body,"China Express Railways", "Thank you for your booking, Enjoy your journey!");
            message.IsBodyHtml = true;

            //Connect to the google smtp client
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("andymehaffy2@gmail.com", "Graham@2015"),
                EnableSsl = true
            };
            client.Send(message);
        }
    }
}