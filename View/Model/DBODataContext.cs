using DBO.Model.DataModel;
using Microsoft.EntityFrameworkCore;


namespace DBO.Model
{
    public class DBODataContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = .\Data\DBO.db");
        }
    }
}
