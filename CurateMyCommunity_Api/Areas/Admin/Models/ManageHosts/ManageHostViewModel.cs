using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageHosts
{
    public class ManageHostViewModel
    {
        public ManageHostViewModel()
        {

        }

        public ManageHostViewModel(Host hostDTO)
        {
            id_hosts = hostDTO.id_hosts;
            host_name = hostDTO.host_name;
            twitter_handle = hostDTO.twitter_handle;
        }

        public int id_hosts { get; set; }
        public string host_name { get; set; }
        public string twitter_handle { get; set; }
        public bool is_selected { get; set; }
    }
}