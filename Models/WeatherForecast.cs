using System.ComponentModel.DataAnnotations;

namespace SyncOverAsync.Models
{
    public class WeatherForecast
    {
        public WeatherForecast(int id, int temperatureC, string? summary)
        {
            Id = id;
            Date = DateTime.Now.AddDays(Id);
            TemperatureC = TemperatureC;
            Summary = summary;
        }

        [Key]
        public int Id{ get; set; }

        public DateTime Date{ get; set; }

        public int TemperatureC{ get; set; }

        public string? Summary{ get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
