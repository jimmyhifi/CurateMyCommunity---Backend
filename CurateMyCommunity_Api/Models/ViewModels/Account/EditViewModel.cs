using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.ViewModels.Account
{
    public class EditViewModel
    {
        [Required]
        public int id_user { get; set; }
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