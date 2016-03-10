using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystem.Models;

namespace CERBookingSystem.Controllers
{
    public class TrainRouteController : Controller
    {
        // GET: TrainRoute
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult newTrain()
        {
            return View();
        }
        public ActionResult newRoute()
        {
            return View();
        }
        [HttpPost]
        public ActionResult newTrain(newTrain train)
        {
            if (ModelState.IsValid)
            {

            }
            return View(train);
        }
        [HttpPost]
        public ActionResult newRoute(newRoute route)
        {
            if (ModelState.IsValid)
            {

            }
            return View(route);
        }
    }
}