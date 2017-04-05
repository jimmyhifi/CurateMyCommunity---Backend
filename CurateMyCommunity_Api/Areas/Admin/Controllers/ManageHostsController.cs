using CurateMyCommunity_Api.Areas.Admin.Models.ManageHosts;
using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurateMyCommunity_Api.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageHostsController : Controller
    {
        // GET: Admin/ManageHosts
        public ActionResult Index()
        {
            List<ManageHostViewModel> collectionOfHostsVM = new List<ManageHostViewModel>();
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Get all Sessions
                var dbHost = context.Hosts;

                //Move Sessions into a viewmodel object
                foreach (var hostDTO in dbHost)
                {
                    collectionOfHostsVM.Add(
                        new ManageHostViewModel(hostDTO)
                        );
                }
            }
            //send viewmodel collection to the View
            return View(collectionOfHostsVM);
        }

        [HttpGet]
        // GET: Admin/ManageHosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageHosts/Create
        [HttpPost]
        public ActionResult Create(CreateHostViewModel newHost)
        {
            //Validate the New User

            //Check that required fields are set
            if (!ModelState.IsValid)
            {
                return View(newHost);
            }
            //Create an instance of our DbContext
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Make sure the username is unique
                if (context.Hosts.Any(row => row.host_name.Equals(newHost.host_name)))
                {
                    ModelState.AddModelError("", "Host already exists. Try Again.");
                    newHost.host_name = "";
                    return View(newHost);
                }
                //Create our session DTO
                Host newHostDTO = new CurateMyCommunity_Api.Models.Data.Host()
                {
                    host_name = newHost.host_name,
                    twitter_handle = newHost.twitter_handle

                };
                //Add to DbContext
                newHostDTO = context.Hosts.Add(newHostDTO);
                //Save Changes
                context.SaveChanges();
            }
            //Redirect to login page
            return RedirectToAction("Index");
        }

        [HttpGet]
        // GET: Admin/ManageHosts/Edit/5
        public ActionResult Edit(int id)
        {
            EditHostViewModel editHostVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get user from database
                Host hostDTO = context.Hosts.Find(id);

                if (hostDTO == null)
                {
                    return Content("Invalid Id");
                }
                // create a EditViewModel
                editHostVM = new EditHostViewModel()
                {
                    id_hosts = hostDTO.id_hosts,
                    host_name = hostDTO.host_name,
                    twitter_handle = hostDTO.twitter_handle
                };
            }
            // Send the viewmodel to the view
            return View(editHostVM);
        }

        // POST: Admin/ManageHosts/Edit/5
        [HttpPost]
        public ActionResult Edit(EditHostViewModel editVM)
        {
            //Validate Model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            //get session from the database
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                Host hostDTO = context.Hosts.Find(editVM.id_hosts);

                if (hostDTO == null) { return Content("Invalid Host"); }

                //set update values
                hostDTO.host_name = editVM.host_name;
                hostDTO.twitter_handle = editVM.twitter_handle;

                //save changes
                context.SaveChanges();

            }


            return RedirectToAction("Index");
        }

        // POST: Admin/ManageHosts/Delete/5
        [HttpPost]
        public ActionResult Delete(List<ManageHostViewModel> collectionOfHostsVM)
        {
            //filter collection of users on is selected
            var vmSessionsToDelete = collectionOfHostsVM.Where(x => x.is_selected == true);

            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //loop through view models items
                foreach (var vmItem in vmSessionsToDelete)
                {
                    var dtoToDelete = context.Hosts.FirstOrDefault(row => row.id_hosts == vmItem.id_hosts);
                    context.Hosts.Remove(dtoToDelete);

                }
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
