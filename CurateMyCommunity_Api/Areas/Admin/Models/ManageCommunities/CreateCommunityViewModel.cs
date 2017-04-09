using CurateMyCommunity_Api.Areas.Admin.Models.ManageHosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunities
{
    public class CreateCommunityViewModel
    {
        public string community { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public int id_host { get; set; }
    }
}