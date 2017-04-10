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

        public ActionResult UserProfile(int? id = null)
        {
            // Capture Logged in User
            string username = User.Identity.Name;

            // Retrieve the user from database
            UserProfileViewModel profileVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get User from the database
                User userDTO;
                if (id.HasValue)
                {
                    userDTO = context.Users.Find(id.Value);
                }
                else
                {
                    userDTO = context.Users.FirstOrDefault(row => row.username == username);
                }

                if (userDTO == null)
                {
                    return Content("Invalid Username");
                }
                // populate our UserProfileViewModel
                profileVM = new UserProfileViewModel()
                {
                    dateCreated = userDTO.date_created,
                    email = userDTO.email,
                    firstname = userDTO.firstname,
                    id_user = userDTO.id_users,
                    lastname = userDTO.lastname,
                    username = userDTO.username
                };
            }

            // Return the view with the ViewModel
            return View(profileVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Get the user by Id

            EditViewModel editVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get user from database
                User userDTO = context.Users.Find(id);

                if (userDTO == null)
                {
                    return Content("Invalid Id");
                }
                // create a EditViewModel
                editVM = new EditViewModel()
                {
                    id_user = userDTO.id_users,
                    email = userDTO.email,
                    firstname = userDTO.firstname,
                    lastname = userDTO.lastname,
                    username = userDTO.username
                };
            }
            // Send the viewmodel to the view
            return View(editVM);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel editVM)
        {
            //Variables
            bool needsPasswordReset = false;
            bool usernameHasChanged = false;

            // Validate Model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }

            //Check for password change
            if (!string.IsNullOrWhiteSpace(editVM.password))
            {
                //Compare password with password confirm
                if (editVM.password != editVM.passwordConfirm)
                {
                    ModelState.AddModelError("", "Password and PasswordConfirm must match");
                    return View(editVM);
                }
                else
                {
                    needsPasswordReset = true;
                }
            }
            // get our user form the database
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get our DTO
                User userDTO = context.Users.Find(editVM.id_user);
                if (userDTO == null) { return Content("Invalid User Id"); }

                // CHeck for username change
                if (userDTO.username != editVM.username)
                {

                    userDTO.username = editVM.username;
                    usernameHasChanged = true;
                }

                // set/update values from the viewmodel
                userDTO.firstname = editVM.firstname;
                userDTO.email = editVM.email;
                userDTO.lastname = editVM.lastname;

                if (needsPasswordReset)
                {
                    userDTO.password = PasswordStorage.CreateHash(editVM.password);
                }
                // save changes
                context.SaveChanges();
            }
            if (usernameHasChanged || needsPasswordReset)
            {
                TempData["LogoutMessage"] = "After a username or password change. Please log in with the new credentials.";
                return RedirectToAction("Logout");
            }
            return RedirectToAction("UserProfile");
        }
    }
}