using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunties
{
    public class CreateCommunityViewModel
    {
        public string community { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string host { get; set; }

        public string twitter_handle { get; set; }
    }
}