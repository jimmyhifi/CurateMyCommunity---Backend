using CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunities;
using CurateMyCommunity_Api.Areas.Admin.Models.ManageImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageExhibits
{
    public class CreateExhibitViewModel
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int id_community { get; set; }
        public int id_image { get; set; }

    }
}