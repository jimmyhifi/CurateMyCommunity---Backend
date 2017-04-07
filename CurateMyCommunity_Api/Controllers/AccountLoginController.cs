using CurateMyCommunity_Api.Areas.Admin.Models.ManageUsers;
using CurateMyCommunity_Api.Models.Data;
using CurateMyCommunity_Api.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CurateMyCommunity_Api.Controllers
{
    public class AccountLoginController : Controller
    {
        // GET: AccountLogin
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginUserViewModel loginUser)
        {
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Login is Required");
                return View();
            }

            if (string.IsNullOrWhiteSpace(loginUser.username))
            {
                ModelState.AddModelError("", "Username is Required");
                return View();
            }

            if (string.IsNullOrWhiteSpace(loginUser.password))
            {
                ModelState.AddModelError("", "Password is Required");
                return View();
            }

            //Open database connection
            bool isValid = false;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //search for logging in user
                Models.Data.User UserDTO = context.Users.FirstOrDefault(x => x.username == loginUser.username);
                // get current users hashed password
                string correcthash = UserDTO.password;
                //hash password
                bool passwordhash = PasswordStorage.VerifyPassword(loginUser.password, correcthash);
                //query for user based on username and password hash
                if (context.Users.Any(row => row.username.Equals(loginUser.username)) && passwordhash)
                {
                    isValid = true;
                }
            }

            //if invalid, send error
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            else
            {
                //valid, redirect to user profile

                System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.username, loginUser.rememberMe);
                FormsAuthentication.GetRedirectUrl(loginUser.username, loginUser.rememberMe);
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            //HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            return RedirectToAction("Login");
        }

        public ActionResult UserNavPartial()
        {
            // capture logged in user
            string username;
            username = this.User.Identity.Name;

            // get user information form database
            UserNavPartialViewModel userNavVM;

            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // search for user
                Models.Data.User UserDTO = context.Users.FirstOrDefault(x => x.username == username);

                if (UserDTO == null) { return Content(""); }
                // build our UserNavPartialViewModel
                userNavVM = new UserNavPartialViewModel()
                {
                    FirstName = UserDTO.firstname,
                    LastName = UserDTO.lastname,
                    Id = UserDTO.id_users
                };
            }

            // Build our usernavPartial viewmodel

            // send the view model to the patriel view

            return PartialView(userNavVM);
        }
    }
}