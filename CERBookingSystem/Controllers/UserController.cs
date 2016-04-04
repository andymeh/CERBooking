using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CERBookingSystem.Models;
using System.Web.Security;
using CERBookingSystemBLL;
using CERBookingSystemDAL;

namespace CERBookingSystem.Controllers
{
    [AllowAnonymous]
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

        [Authorize]
        public ActionResult RegisterNewEmployee()
        {
            User user = UserBLL.getUser(User.Identity.Name);
            if (user.Employee)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult Login()
        {
            var model = new UserLogin();
            return PartialView("Login", model);
        }

        [HttpPost]
        public ActionResult Register(newUserModel newUser)
        {
            if (ModelState.IsValid)
            {
                if (!UserBLL.UserNameExists(newUser.UserEmail))
                {
                    if (newUser.UserEmail.Equals(newUser.UserEmail2))
                    {
                        if (newUser.Password.Equals(newUser.Password2))
                        {
                            User dalUser = new CERBookingSystemDAL.User{
                                EmailAddress = newUser.UserEmail,
                                Password = SHA1.Encode(newUser.Password),
                                Forename = newUser.Forename,
                                Surname = newUser.Surname,
                                Employee = false
                            };
                            UserBLL.addNewUser(dalUser);
                            FormsAuthentication.SetAuthCookie(dalUser.EmailAddress, false);
                            RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Passwords did not match!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Addresses did not match!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email Address is already registered!");
                }
            }
            return View(newUser);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RegisterNewEmployee(newUserModel newUser)
        {
            if (ModelState.IsValid)
            {
                User user = UserBLL.getUser(User.Identity.Name);
                if (user.Employee)
                {
                    if (!UserBLL.UserNameExists(newUser.UserEmail))
                    {
                        if (newUser.UserEmail.Equals(newUser.UserEmail2))
                        {
                            if (newUser.Password.Equals(newUser.Password2))
                            {
                                User dalUser = new CERBookingSystemDAL.User
                                {
                                    EmailAddress = newUser.UserEmail,
                                    Password = SHA1.Encode(newUser.Password),
                                    Forename = newUser.Forename,
                                    Surname = newUser.Surname,
                                    Employee = true
                                };
                                UserBLL.addNewUser(dalUser);
                                RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Passwords did not match!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email Addresses did not match!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Address is already registered!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You do not have permission to perform this operation!");
                }
            }
            return View(newUser);
        }

        [Authorize]
        public string UserName(string emailAddress)
        {
            string username = "";
            if(emailAddress != null)
            {
                User user = UserBLL.getUser(emailAddress);
                username = user.Forename + " " + user.Surname;
            }
            return username;
        }

        [Authorize]
        [Route("IsEmployee")]
        public string IsEmployee(string emailAddress)
        {
            string username = "False";
            if (emailAddress != null)
            {
                User user = UserBLL.getUser(emailAddress);
                username = user.Employee.ToString();
            }
            return username;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}