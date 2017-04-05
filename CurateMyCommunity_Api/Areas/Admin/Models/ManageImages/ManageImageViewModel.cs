using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageImages
{
    public class ManageImageViewModel
    {
        public ManageImageViewModel()
        {
        }

        public ManageImageViewModel(Image imageDTO)
        {
            id_images = imageDTO.id_images;
            image_url = imageDTO.image_url;

        }

        public int id_images { get; set; }

        public string image_url { get; set; }
        public bool is_selected { get; set; }
    }
}