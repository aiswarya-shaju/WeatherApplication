using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication.Models.Models
{
    public class WeatherInfo
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Invalid City Provided")]
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
        public string Unit { get; set; }
    }

    public class WeatherHistory
    {
        public int Id { get; set; }  // ✅ EF Core requires this or a [Key] annotation on another property
        public string City { get; set; }
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public double Wind { get; set; }
        public int Humidity { get; set; }
        public string Unit { get; set; }
    }

    public class WeatherInfoResponse
    {
        public MainInfo Main { get; set; }
        public List<WeatherDescription> Weather { get; set; }
        public WindInfo Wind { get; set; }
        public string Name { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }

}
