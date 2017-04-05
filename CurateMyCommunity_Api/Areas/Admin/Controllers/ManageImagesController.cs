using CurateMyCommunity_Api.Areas.Admin.Models.ManageImages;
using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurateMyCommunity_Api.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageImagesController : Controller
    {
        // GET: Admin/ManageImages
        public ActionResult Index()
        {
            List<ManageImageViewModel> collectionOfImagesVM = new List<ManageImageViewModel>();
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Get all Sessions
                var dbImage = context.Images;

                //Move Sessions into a viewmodel object
                foreach (var imageDTO in dbImage)
                {
                    collectionOfImagesVM.Add(
                        new ManageImageViewModel(imageDTO)
                        );
                }
            }
            //send viewmodel collection to the View
            return View(collectionOfImagesVM);
        }

        [HttpGet]
        // GET: Admin/ManageImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageImages/Create
        [HttpPost]
        public ActionResult Create(CreateImageViewModel newImage)
        {
            //Validate the New User

            //Check that required fields are set
            if (!ModelState.IsValid)
            {
                return View(newImage);
            }
            //Create an instance of our DbContext
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //Make sure the username is unique
                if (context.Images.Any(row => row.image_url.Equals(newImage.image_url)))
                {
                    ModelState.AddModelError("", "Image already exists. Try Again.");
                    newImage.image_url = "";
                    return View(newImage);
                }
                //Create our session DTO
                Image newImageDTO = new CurateMyCommunity_Api.Models.Data.Image()
                {
                    image_url = newImage.image_url

                };
                //Add to DbContext
                newImageDTO = context.Images.Add(newImageDTO);
                //Save Changes
                context.SaveChanges();
            }
            //Redirect to login page
            return RedirectToAction("Index");
        }

        // GET: Admin/ManageImages/Edit/5
        public ActionResult Edit(int id)
        {
            EditImageViewModel editImageVM;
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                // Get user from database
                Image imageDTO = context.Images.Find(id);

                if (imageDTO == null)
                {
                    return Content("Invalid Id");
                }
                // create a EditViewModel
                editImageVM = new EditImageViewModel()
                {
                    id_images = imageDTO.id_images,
                    image_url = imageDTO.image_url
                };
            }
            // Send the viewmodel to the view
            return View(editImageVM);
        }

        // POST: Admin/ManageImages/Edit/5
        [HttpPost]
        public ActionResult Edit(EditImageViewModel editVM)
        {
            //Validate Model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            //get session from the database
            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                Image imageDTO = context.Images.Find(editVM.id_images);

                if (imageDTO == null) { return Content("Invalid Image"); }

                //set update values
                imageDTO.image_url = editVM.image_url;

                //save changes
                context.SaveChanges();

            }


            return RedirectToAction("Index");
        }
        // POST: Admin/ManageImages/Delete/5
        [HttpPost]
        public ActionResult Delete(List<ManageImageViewModel> collectionOfImagesVM)
        {
            //filter collection of users on is selected
            var vmImagesToDelete = collectionOfImagesVM.Where(x => x.is_selected == true);

            using (CMC_DB_Connection context = new CMC_DB_Connection())
            {
                //loop through view models items
                foreach (var vmItem in vmImagesToDelete)
                {
                    var dtoToDelete = context.Images.FirstOrDefault(row => row.id_images == vmItem.id_images);
                    context.Images.Remove(dtoToDelete);

                }
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
