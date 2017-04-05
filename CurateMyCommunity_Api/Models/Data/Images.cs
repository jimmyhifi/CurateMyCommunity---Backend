using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_images")]
    public class Image
    {
        [Key, Column("id_images"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_images { get; set; }

        public string image_url { get; set; }
    }
}