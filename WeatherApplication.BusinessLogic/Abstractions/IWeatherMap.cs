using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Models.Models;

namespace WeatherApplication.BusinessLogic.Abstractions
{
    public interface IWeatherMap
    {
        Task<WeatherInfo> FetchCity(WeatherInfo cityModel);
        Task<List<WeatherHistory>> GetUserHistory();
    }
}
