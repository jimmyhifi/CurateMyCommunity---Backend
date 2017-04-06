using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_roles")]
    public class Role
    {
        [Key, Column("id_roles"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_roles { get; set; }

        public string role_name { get; set; }
        public bool is_admin { get; set; }
    }
}