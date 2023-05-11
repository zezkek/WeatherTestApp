using Microsoft.EntityFrameworkCore;
using WeatherTestApp.Models;

namespace WeatherTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<WeatherPeriod> WeatherPeriods { get; set; }
    }
}
