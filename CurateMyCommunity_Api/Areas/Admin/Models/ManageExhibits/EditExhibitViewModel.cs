using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageExhibits
{
    public class EditExhibitViewModel
    {
        public int id_exhibits { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}