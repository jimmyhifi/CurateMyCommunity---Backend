﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunties
{
    public class EditCommunityViewModel
    {
        [Required]
        public int id_communities { get; set; }
        public string community { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string host { get; set; }

        public string twitter_handle { get; set; }
    }
}