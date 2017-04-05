using CurateMyCommunity_Api.Models.Data;
using CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurateMyCommunity_Api.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageCommunitiesController : Controller
    {
        // GET: Admin/ManageCommunity
        [HttpGet]
        public ActionResult Index()
        {
            List<ManageCommunityViewModel> collectionOfSessionsVM = new List<ManageCommunityViewModel>();
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Get all Sessions
                var dbCommunity = context.Communities;

                //Move Sessions into a viewmodel object
                foreach (var communityDTO in dbCommunity)
                {
                    collectionOfSessionsVM.Add(
                        new ManageCommunityViewModel(communityDTO)
                        );
                }
            }
            //send viewmodel collection to the View
            return View(collectionOfSessionsVM);
        }

        // GET: Admin/ManageCommunity/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageCommunity/Create
        [HttpPost]
        public ActionResult Create(CreateCommunityViewModel newCommunity)
        {
            //Validate the New User

            //Check that required fields are set
            if (!ModelState.IsValid)
            {
                return View(newCommunity);
            }
            //Create an instance of our DbContext
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Make sure the username is unique
                if (context.Communities.Any(row => row.community.Equals(newCommunity.community)))
                {
                    ModelState.AddModelError("", "Community already exists. Try Again.");
                    newCommunity.community = "";
                    return View(newCommunity);
                }
                //Create our session DTO
                Community newCommunityDTO = new CurateMyCommunity_Api.Models.Data.Community()
                {
                    community = newCommunity.community,
                    city = newCommunity.city,
                    state = newCommunity.state
            };
                //Add to DbContext
                newCommunityDTO = context.Communities.Add(newCommunityDTO);
                //Save Changes
                context.SaveChanges();
            }
            //Redirect to login page
            return RedirectToAction("Index");
        }

        // GET: Admin/ManageCommunity/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Get the session by Id

            EditCommunityViewModel editCommunityVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get user from database
                Community communityDTO = context.Communities.Find(id);

                if (communityDTO == null)
                {
                    return Content("Invalid Id");
                }
                // create a EditViewModel
                editCommunityVM = new EditCommunityViewModel()
                {
                    id_communities = communityDTO.id_communities,
                    community = communityDTO.community,
                    city = communityDTO.city,
                    state = communityDTO.state
            };
            }
            // Send the viewmodel to the view
            return View(editCommunityVM);
        }

        // POST: Admin/ManageCommunity/Edit/5
        [HttpPost]
        public ActionResult Edit(EditCommunityViewModel editVM)
        {
            //Validate Model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            //get session from the database
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                Community communityDTO = context.Communities.Find(editVM.id_communities);

                if (communityDTO == null) { return Content("Invalid Community"); }

                //set update values
                communityDTO.community = editVM.community;
                communityDTO.city = editVM.city;
                communityDTO.state = editVM.state;

                //save changes
                context.SaveChanges();

            }


            return RedirectToAction("Index");
        }

        // GET: Admin/ManageCommunity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ManageCommunity/Delete/5
        [HttpPost]
        public ActionResult Delete(List<ManageCommunityViewModel> collectionOfCommunityVM)
        {
            //filter collection of users on is selected
            var vmSessionsToDelete = collectionOfCommunityVM.Where(x => x.is_selected == true);

            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //loop through view models items
                foreach (var vmItem in vmSessionsToDelete)
                {
                    var dtoToDelete = context.Communities.FirstOrDefault(row => row.id_communities == vmItem.id_communities);
                    context.Communities.Remove(dtoToDelete);

                }
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
