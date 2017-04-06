using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_user_roles")]
    public class UserRole
    {
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column("tbl_id_users", Order = 1)]
        public int id_user_roles { get; set; }

        [Column("tbl_id_roles", Order = 2)]
        public int tbl_id_roles { get; set; }

        [ForeignKey("tbl_id_roles")]
        public virtual Role RoleId { get; set; }

        [ForeignKey("id_user_roles")]
        public virtual User UserId { get; set; }
    }
}