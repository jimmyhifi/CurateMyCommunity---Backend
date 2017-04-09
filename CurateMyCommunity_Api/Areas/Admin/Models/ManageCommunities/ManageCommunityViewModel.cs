using CurateMyCommunity_Api.Areas.Admin.Models.ManageHosts;
using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunities
{
    public class ManageCommunityViewModel
    {
        public ManageCommunityViewModel()
        {

        }

        public ManageCommunityViewModel(Community communityDTO)
        {
            id_communities = communityDTO.id_communities;
            community = communityDTO.community;
            city = communityDTO.city;
            state = communityDTO.state;
            id_host = communityDTO.tbl_id_hosts;
        }

        public int id_communities { get; set; }
        public string community { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int id_host { get; set; }
        public bool is_selected { get; set; }
    }
}