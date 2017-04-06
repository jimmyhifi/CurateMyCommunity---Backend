using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_users")]
    public class User
    {
        [Key, Column("id_users"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_users { get; set; }
        public Nullable<int> firebase_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        [Column("date_created")]
        public DateTime date_created { get; set; }
    }
}