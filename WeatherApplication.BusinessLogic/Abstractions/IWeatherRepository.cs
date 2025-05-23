using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Models.Models;

namespace WeatherApplication.BusinessLogic.Abstractions
{
    public interface IWeatherRepository
    {
        Task<WeatherInfo> GetWeatherAsync(string city, string unit);
        Task<List<WeatherHistory>> GetUserHistory();
    }
}
