using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageUsers
{
    public class ManageUserViewModel
    {
        public ManageUserViewModel()
        {

        }

        public ManageUserViewModel(User userDTO)
        {
            id_users = userDTO.id_users;
            firstname = userDTO.firstname;
            lastname = userDTO.lastname;
            email = userDTO.email;
            username = userDTO.username;
            password = userDTO.password;
            date_created = userDTO.date_created;
        }
        
        public int id_users { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime date_created { get; set; }
        public bool is_selected { get; set; }
    }
}