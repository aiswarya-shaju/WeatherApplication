using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.DataAccess.Abstractions;
using WeatherApplication.Models.Models;

namespace WeatherApplication.DataAccess.Implementations
{
    public class UserHistoryData : IUserHistoryData
    {
        private readonly IUserHistory UserHistory;
        public UserHistoryData(IUserHistory userHistory)
        {
            UserHistory = userHistory;
        }
        public Task<IEnumerable<WeatherHistory>> GetUserHistory() =>
            UserHistory.GetUserHistoryAsync<WeatherHistory, dynamic>(storedProcedure: "dbo.GetAllHistory", new { });
        
        public async Task<bool> AddUserHistory(WeatherHistory userHistory) //City, Unit, Time, Temperature, Humidity, Wind
        {
            var temperature = Convert.ToDecimal(userHistory.Temperature);
            var wind = Convert.ToDecimal(userHistory.Wind);
            var result = await UserHistory.AddUserHistoryAsync("dbo.AddHistory", new { userHistory.City, userHistory.Unit, userHistory.Time, temperature, userHistory.Humidity, wind });
            return result;
        }
            //UserHistory.AddUserHistoryAsync<WeatherHistory>(storedProcedure: "dbo.AddUserHistory", new { userHistory.City, userHistory.Unit, userHistory.Time, userHistory.Temperature, userHistory.Humidity, userHistory.WindSpeed});
    }
}