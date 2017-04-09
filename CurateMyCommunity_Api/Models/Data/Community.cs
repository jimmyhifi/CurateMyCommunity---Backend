using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_communities")]
    [DataContract(IsReference = false)]
    public class Community
    {
        [Key, Column("id_communities"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int id_communities { get; set; }

        [DataMember]
        public string community { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string state { get; set; }

        [DataMember]
        [Column("tbl_id_hosts", Order = 1)]
        public int tbl_id_hosts { get; set; }

        [ForeignKey("tbl_id_hosts")]
        public virtual Community id_host { get; set; }

    }
}