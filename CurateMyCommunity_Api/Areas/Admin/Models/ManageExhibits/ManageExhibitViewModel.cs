using CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunities;
using CurateMyCommunity_Api.Areas.Admin.Models.ManageImages;
using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageExhibits
{
    public class ManageExhibitViewModel
    {
        public ManageExhibitViewModel()
        {

        }

        public ManageExhibitViewModel(Exhibit exhibitDTO)
        {
            id_exhibits = exhibitDTO.id_exhibits;
            latitude = exhibitDTO.latitude;
            longitude = exhibitDTO.longitude;
            name = exhibitDTO.name;
            description = exhibitDTO.description;
            id_community = exhibitDTO.tbl_id_communities;
            id_image = exhibitDTO.tbl_id_images;
        }

        public int id_exhibits { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int id_community { get; set; }
        public int id_image { get; set; }
        public bool is_selected { get; set; }

    }
}