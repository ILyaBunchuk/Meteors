using MeteorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MeteorApp.DB
{
    public class MeteorContext : DbContext
    {
        public DbSet<MeteorDB> Meteors { get; set; }
        public MeteorContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=1111;database=meteordb;",
                new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}
