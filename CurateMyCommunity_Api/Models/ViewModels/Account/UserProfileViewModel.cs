using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public int id_user { get; set; }
        public string  firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public DateTime dateCreated { get; set; }
    }
}