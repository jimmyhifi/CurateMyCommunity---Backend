using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Areas.Admin.Models.ManageUsers
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {

        }

        public EditUserViewModel(User userDTO)
        {
            id_users = userDTO.id_users;
            firstname = userDTO.firstname;
            lastname = userDTO.lastname;
            email = userDTO.email;
            username = userDTO.username;
            password = userDTO.password;
            passwordConfirm = userDTO.password;
        }

        [Required]
        public int id_users { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string username { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }

    }
}