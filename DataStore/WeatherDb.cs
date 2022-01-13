using Microsoft.EntityFrameworkCore;
using SyncOverAsync.Models;

namespace SyncOverAsync.DataStore
{
    public class WeatherDb : DbContext
    {
        public WeatherDb(DbContextOptions options) : base(options) { }

        public DbSet<WeatherForecast> Weather { get; set; }
    }
}
