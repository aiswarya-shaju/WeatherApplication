using Microsoft.EntityFrameworkCore;
using WeatherApplication.Models.Models;

namespace WeatherApplication.BusinessLogic.Implementations
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<WeatherHistory> UserHistories { get; set; }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherHistory>().ToTable("UserHistory");
        }*/
    }
}
