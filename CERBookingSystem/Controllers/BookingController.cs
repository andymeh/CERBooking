using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystem.Models;

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

        public ActionResult NewBooking(NewBookingModel newBooking)
        {
            if(ModelState.IsValid)
            {

            }
            return View();
        }
    }
}