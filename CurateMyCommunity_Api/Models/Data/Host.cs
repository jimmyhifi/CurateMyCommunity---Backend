using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CurateMyCommunity_Api.Models.Data
{
    [Table("tbl_hosts")]
    public class Host
    {
        [Key]
        public int id_hosts { get; set; }
        public string host_name { get; set; }
        public string twitter_handle { get; set; }
    }
}