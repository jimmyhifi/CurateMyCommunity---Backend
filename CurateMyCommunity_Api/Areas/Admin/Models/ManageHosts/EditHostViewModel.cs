using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageHosts
{
    public class EditHostViewModel
    {
        public int id_hosts { get; set; }
        public string host_name { get; set; }
        public string twitter_handle { get; set; }
    }
}