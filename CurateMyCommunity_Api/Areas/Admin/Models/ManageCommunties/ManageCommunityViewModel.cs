using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageCommunties
{
    public class ManageCommunityViewModel
    {
        public ManageCommunityViewModel()
        {

        }

        public ManageCommunityViewModel(Community communityDTO)
        {
            id_communities = communityDTO.id_communties;
            community = communityDTO.community;
            city = communityDTO.city;
            state = communityDTO.state;
            host = communityDTO.host;
            twitter_handle = communityDTO.twitter_handle;
        }

        public int id_communities { get; set; }
        public string community { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string host { get; set; }
        public string twitter_handle { get; set; }
        public bool is_selected { get; set; }
    }
}