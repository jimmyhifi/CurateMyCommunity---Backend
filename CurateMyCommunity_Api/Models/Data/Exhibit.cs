using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_exhibits")]
    public class Exhibit
    {
        [Key, Column("id_exhibits", Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_exhibits { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        [Column("tbl_id_communities", Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tbl_id_communities { get; set; }

        [ForeignKey("tbl_id_communities")]
        public virtual Community id_community { get; set; }

        [Column("tbl_id_images", Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tbl_id_images { get; set; }

        [ForeignKey("tbl_id_images")]
        public virtual Community id_images { get; set; }

    }
}