using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystem.Models;

namespace CERBookingSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(newUserModel newUser)
        {
            if (ModelState.IsValid)
            {

            }
            return View(newUser);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                //get user from database
                //if user in database, set authorisation token

                //else return incorrect password error
            }
            return View(login);
        }
    }
}