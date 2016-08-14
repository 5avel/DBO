using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBO.DataModel;
using System.IO;

namespace Model
{
    public class DBODataContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<IpCamera> IpCameras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlite(@"Data Source = .\..\..\..\Data\DBO.db");
        }
    }
}
