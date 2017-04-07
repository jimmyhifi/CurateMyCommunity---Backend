using CurateMyCommunity_Api.Areas.Admin.Models.ManageUsers;
using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CurateMyCommunity_Api.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageUsersController : Controller
    {
        // GET: Admin/ManageUsers
        [HttpGet]
        public ActionResult Index()
        {

            // setup a db context
            List<ManageUserViewModel> collectionOfUserVM = new List<ManageUserViewModel>();
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //get all users
                var dbUsers = context.Users;

                //Move users into a viewmodel object
                foreach (var userDTO in dbUsers)
                {
                    collectionOfUserVM.Add(
                        new ManageUserViewModel(userDTO));
                }
            }

                return View(collectionOfUserVM);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageUsers/Create
        [HttpPost]
        public ActionResult Create(CreateUserViewModel newSession)
        {
            //Validate the new user
            //check that required fields are set
            if (!ModelState.IsValid)
            {
                return View(newSession);
            }

            bool needsPasswordReset = false;

            //check for password and password confirm match
            if (!string.IsNullOrWhiteSpace(newSession.password))
            {
                //Compare password with password confirm
                if (newSession.password != newSession.passwordConfirm)
                {
                    ModelState.AddModelError("", "Password and PasswordConfirm must match");
                    return View(newSession);
                }
                else
                {
                    needsPasswordReset = true;
                }
            }

            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //make sure the username is unique
                if (context.Users.Any(row => row.username.Equals(newSession.username)))
                {
                    ModelState.AddModelError("", "Username already exists. Try Again");
                    newSession.username = "";
                    return View(newSession);
                }
                //create our user dto
                User newUserDTO = new CurateMyCommunity_Api.Models.Data.User()
                {
                    firstname = newSession.firstname,
                    lastname = newSession.lastname,
                    email = newSession.email,
                    username = newSession.username,
                    password = PasswordStorage.CreateHash(newSession.password),
                    date_created = DateTime.Now
                };
                // add to db context
                newUserDTO = context.Users.Add(newUserDTO);
                context.SaveChanges();

            }

                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int userId)
        {
            // Get the user by Id

            EditUserViewModel editVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get user from database
                User userDTO = context.Users.Find(userId);

                if (userDTO == null)
                {
                    return Content("Invalid Id");
                }
                // create a EditViewModel
                editVM = new EditUserViewModel()
                {
                    id_users = userDTO.id_users,
                    email = userDTO.email,
                    firstname = userDTO.firstname,
                    lastname = userDTO.lastname,
                    username = userDTO.username
                };
            }
            // Send the viewmodel to the view
            return View(editVM);
        }

        // POST: Admin/ManageUsers/Edit/
        [HttpPost]
        public ActionResult Edit(EditUserViewModel editVM)
        {
            //Variables
            bool needsPasswordReset = false;

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
                User userDTO = context.Users.Find(editVM.id_users);
                if (userDTO == null) { return Content("Invalid User Id"); }


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
            return RedirectToAction("Index");
        }

        // POST: Admin/ManageUsers/Delete
        [HttpPost]
        public ActionResult Delete(List<ManageUserViewModel> collectionOfUserVM)
        {
            //Filter collectionOfUsers to find the selected items only
            var itemsToDelete = collectionOfUserVM.Where(row => row.is_selected == true);

            // do the delete
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                foreach (var vmItems in itemsToDelete)
                {
                    var dtoToDelete = context.Users.FirstOrDefault(row => row.id_users == vmItems.id_users);
                    context.Users.Remove(dtoToDelete);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
