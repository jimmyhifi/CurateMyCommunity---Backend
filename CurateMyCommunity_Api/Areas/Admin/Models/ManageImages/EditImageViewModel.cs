using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageImages
{
    public class EditImageViewModel
    {
        public int id_images { get; set; }
        public string  image_url { get; set; }
    }
}