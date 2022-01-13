using Microsoft.EntityFrameworkCore;
using SyncOverAsync.Models;
using System.Collections.Generic;

namespace SyncOverAsync.DataStore
{
    public class WeatherDb : DbContext
    {
        public WeatherDb(DbContextOptions options) : base(options) 
        {             
        }
        
        public DbSet<WeatherForecast> Weather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<WeatherForecast> GetWeather() => this.Weather.ToList();

    }
}
