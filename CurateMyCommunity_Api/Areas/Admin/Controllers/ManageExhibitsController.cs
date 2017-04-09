using CurateMyCommunity_Api.Areas.Admin.Models.ManageExhibits;
using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurateMyCommunity_Api.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageExhibitsController : Controller
    {
        // GET: Admin/ManageExhibits
        [HttpGet]
        public ActionResult Index()
        {//Setup a DbContext
            List<ManageExhibitViewModel> collectionOfExhibitsVM = new List<ManageExhibitViewModel>();
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Get all Sessions
                var dbExhibits = context.Exhibits;

                //Move Sessions into a viewmodel object
                foreach (var exhibitDTO in dbExhibits)
                {
                    collectionOfExhibitsVM.Add(
                        new ManageExhibitViewModel(exhibitDTO)
                        );
                }
            }
            //send viewmodel collection to the View
            return View(collectionOfExhibitsVM);
        }

        // GET: Admin/ManageExhibits/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageExhibits/Create
        [HttpPost]
        public ActionResult Create(CreateExhibitViewModel newExhibit)
        {
            //Validate the New exhibit

            //Check that required fields are set
            if (!ModelState.IsValid)
            {
                return View(newExhibit);
            }
            //Create an instance of our DbContext
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Make sure the exhibit name is unique
                if (context.Exhibits.Any(row => row.name.Equals(newExhibit.name)))
                {
                    ModelState.AddModelError("", "Exhibit name already exists. Try Again.");
                    newExhibit.name = "";
                    return View(newExhibit);
                }
                //Create our exhibit DTO
                Exhibit newExhibitDTO = new CurateMyCommunity_Api.Models.Data.Exhibit()
                {
                    latitude = newExhibit.latitude,
                    longitude = newExhibit.longitude,
                    name = newExhibit.name,
                    description = newExhibit.description,
                    tbl_id_communities = newExhibit.id_community,
                    tbl_id_images = newExhibit.id_image
            };
                //Add to DbContext
                newExhibitDTO = context.Exhibits.Add(newExhibitDTO);
                //Save Changes
                context.SaveChanges();
            }
            //Redirect to Exhibit list
            return RedirectToAction("Index");
        }

        // GET: Admin/ManageExhibits/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Get the session by Id

            EditExhibitViewModel editExhibitVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get user from database
                Exhibit exhibitDTO = context.Exhibits.Find(id);

                if (exhibitDTO == null)
                {
                    return Content("Invalid Id");
                }
                // create a EditViewModel
                editExhibitVM = new EditExhibitViewModel()
                {
                    id_exhibits = exhibitDTO.id_exhibits,
                    latitude = exhibitDTO.latitude,
                    longitude = exhibitDTO.longitude,
                    name = exhibitDTO.name,
                    description = exhibitDTO.description,
                    id_community = exhibitDTO.tbl_id_communities,
                    id_image = exhibitDTO.tbl_id_images
                };
            }
            // Send the viewmodel to the view
            return View(editExhibitVM);
        }

        // POST: Admin/ManageExhibits/Edit/5
        [HttpPost]
        public ActionResult Edit(EditExhibitViewModel editVM)
        {
            //Validate Model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            //get session from the database
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                Exhibit sessionDTO = context.Exhibits.Find(editVM.id_exhibits);

                if (sessionDTO == null) { return Content("Invalid Exhibit"); }

                //set update values
                sessionDTO.name = editVM.name;
                sessionDTO.latitude = editVM.latitude;
                sessionDTO.longitude = editVM.longitude;
                sessionDTO.description = editVM.description;
                sessionDTO.tbl_id_communities = editVM.id_community;
                sessionDTO.tbl_id_images = editVM.id_image;

                //save changes
                context.SaveChanges();

            }


            return RedirectToAction("Index");
        }

        // GET: Admin/ManageExhibits/Delete
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/ManageExhibits/Delete
        [HttpPost]
        public ActionResult Delete(List<ManageExhibitViewModel> collectionOfExhibitsVM)
        {
            //filter collection of users on is selected
            var vmExhibitsToDelete = collectionOfExhibitsVM.Where(x => x.is_selected == true);

            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //loop through view models items
                foreach (var vmItem in vmExhibitsToDelete)
                {
                    var dtoToDelete = context.Exhibits.FirstOrDefault(row => row.id_exhibits == vmItem.id_exhibits);
                    context.Exhibits.Remove(dtoToDelete);

                }
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
