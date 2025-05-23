using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApplication.BusinessLogic.Abstractions;
using WeatherApplication.DataAccess.Abstractions;
using WeatherApplication.Models.Models;

namespace WeatherApplication.BusinessLogic.Implementations
{
    public class WeatherMap: IWeatherMap
    {
        IWeatherRepository WeatherRepository;
        IUserHistoryData UserHistoryData;
        public WeatherMap(IWeatherRepository weatherRepository, IUserHistoryData userHistoryData)
        {
            WeatherRepository = weatherRepository;
            UserHistoryData = userHistoryData;
        }
        public async Task<WeatherInfo> FetchCity(WeatherInfo weatherInfo)
        {
            // Logic to fetch city details
            // This is a placeholder for the actual implementation
            Console.WriteLine($"Fetching details for city: {weatherInfo.City}");
            var city = await WeatherRepository.GetWeatherAsync(weatherInfo.City, weatherInfo.Unit);
            var historyResult = await UserHistoryData.AddUserHistory(new WeatherHistory
            {
                City = weatherInfo.City,
                Unit = weatherInfo.Unit,
                Time = DateTime.Now,
                Temperature = city.Temperature,
                Humidity = city.Humidity,
                Wind = city.WindSpeed
            });
            if (historyResult)
            {
                Console.WriteLine($"History added for city: {weatherInfo.City}");
            }
            else
            {
                Console.WriteLine($"Failed to add history for city: {weatherInfo.City}");
            }
            return city;
        }
        public async Task<List<WeatherHistory>> GetUserHistory()
        {
            List<WeatherHistory> history = new List<WeatherHistory>();
            var result = await UserHistoryData.GetUserHistory();
            if (result != null)
            {
                foreach (var item in result)
                {
                    history.Add(new WeatherHistory
                    {
                        City = item.City,
                        Unit = item.Unit,
                        Time = item.Time,
                        Temperature = item.Temperature,
                        Humidity = item.Humidity,
                        Wind = item.Wind
                    });
                }
            }
            return history;
        }
    }
}
