using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CurateMyCommunity_Api.Models.Data
{
    public class CMC_DB_Connection : DbContext
    {
        public DbSet<Community> Communities { get; set; }

        public DbSet<Exhibit> Exhibits { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public System.Data.Entity.DbSet<CurateMyCommunity_Api.Models.Data.Host> Hosts { get; set; }

        public System.Data.Entity.DbSet<CurateMyCommunity_Api.Models.Data.Image> Images { get; set; }
    }
}