using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WeatherApplication.BusinessLogic.Abstractions;
using WeatherApplication.Models.Models;


namespace WeatherApplication.BusinessLogic.Implementations
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext _context;
        private readonly HttpClient _httpClient;
        //private readonly string apiKey = Configuration.GetValue("OpenWeatherMap:ApiKey");
        //private readonly string apiKey = "2b5a14301345273f4c4e50a17920a9b2";
        string apiKey = string.Empty;
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public WeatherRepository(HttpClient httpClient, WeatherDbContext context, IConfiguration configuration) //WeatherDbContext context)
        {
            //_context = context;
            _httpClient = httpClient;
            _context = context;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DevDB");
            apiKey = _configuration.GetSection("OpenWeatherMap")["ApiKey"];
        }

        public async Task<WeatherInfo> GetWeatherAsync(string city, string unit)
        {
            try
            {
                var encodedCity = Uri.EscapeDataString(city);
                var geoUrl = $"{_configuration.GetSection("OpenWeatherMap")["GeocoderBaseAPI"]}?q={encodedCity}&limit=5&appid={apiKey}";
                List<GeoLocationModel> geoResponses = await _httpClient.GetFromJsonAsync<List<GeoLocationModel>>(geoUrl);

                if (geoResponses == null || geoResponses.Count == 0)
                    throw new Exception("Location not found.");

                double lat = geoResponses[0].Lat;
                double lon = geoResponses[0].Lon;

                if (string.IsNullOrEmpty(unit))
                {
                    unit = "Standard";
                }

                var weatherUrl = (unit.ToLower() == "standard") ? $"{_configuration.GetSection("OpenWeatherMap")["WeatherBaseAPI"]}?lat={lat}&lon={lon}&appid={apiKey}": $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units={unit.ToLower()}";

                var weatherData = await _httpClient.GetFromJsonAsync<WeatherInfoResponse>(weatherUrl);

                if (weatherData == null || weatherData.Weather == null || weatherData.Weather.Count == 0)
                {
                    throw new Exception("Invalid response from weather API.");
                }

                return new WeatherInfo
                {
                    City = city,
                    Temperature = weatherData.Main.Temp,
                    Description = weatherData.Weather[0].Description,
                    WindSpeed = weatherData.Wind.Speed,
                    Humidity = weatherData.Main.Humidity,
                    Unit = unit
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch weather data.", ex);
            }
        }

        /*public async Task SaveSearchHistoryAsync(string city)
        {
            var history = new SearchHistory { City = city, SearchedAt = DateTime.UtcNow };
            _context.SearchHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SearchHistory>> GetSearchHistoryAsync()
        {
            return await _context.SearchHistories.OrderByDescending(x => x.SearchedAt).ToListAsync();
        }

        public async Task<UserPreference> GetUserPreferenceAsync()
        {
            return await _context.UserPreferences.FirstOrDefaultAsync() ?? new UserPreference { Unit = "Celsius", WindSpeedUnit = "km/h", Notify = false };
        }

        public async Task SaveUserPreferenceAsync(UserPreference preference)
        {
            var existing = await _context.UserPreferences.FirstOrDefaultAsync();
            if (existing != null)
            {
                existing.Unit = preference.Unit;
                existing.WindSpeedUnit = preference.WindSpeedUnit;
                existing.Notify = preference.Notify;
            }
            else
            {
                _context.UserPreferences.Add(preference);
            }

            await _context.SaveChangesAsync();
        }*/

        public async Task<List<WeatherHistory>> GetUserHistory()
        {
            return await _context.UserHistories
                                 .OrderByDescending(h => h.Time)
                                 .ToListAsync();
        }
        /*public async Task<List<WeatherHistory>> GetUserHistory()
        {

        }*/
    }
}

